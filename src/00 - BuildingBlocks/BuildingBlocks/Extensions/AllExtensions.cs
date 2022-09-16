using System.Reflection;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using BuildingBlocks.Extensions;
using BuildingBlocks.Modules;

using Dapper.Contrib.Extensions;

using Data.EF.DatabaseContext;
using Data.EF.Repositories.Concretes;
using Data.EF.Repositories.Interfaces;
using Data.Shared.Extensions;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.Extensions;

public static class AllExtensions
{
    public static void InitAutofac(this IHostBuilder hostBuilder) =>
        hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    public static void AddTableNameMapper(this IServiceCollection _)
    {
        SqlMapperExtensions.TableNameMapper = entityType =>
        {
            var getNames = TableMappingName.GetNames();

            var getTable = getNames.FirstOrDefault(_ => _.Name.Equals(entityType.Name, StringComparison.CurrentCulture));

            if (getTable is null)
                throw new Exception($"Not supported entity type {entityType} .. !!!!");

            var result = $"{getTable.SchemaName}.{getTable.PluralName}";

            return result;
        };
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var cnn = configuration.GetConnectionString("DefaultConnection");

        return services
            .AddDbContextPool<AppDbContext>(_ =>
                _.UseInMemoryDatabase(cnn)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            )
        ;
    }

    public static IServiceCollection AddDIContainerBuilder(this IServiceCollection services, IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MyApplicationModule()));

        return services.AddScoped(typeof(IEFRepository<>), typeof(EFRepository<>));
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services) => services.AddAutoMapper(Assembly.GetExecutingAssembly());

    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EventLoggerBehavior<,>));

        //services.AddSingleton<IEventStoreDbContext, EventStoreDbContext>();

        //services.AddTransient(typeof(IRequestPreProcessor<,>), typeof(CustomerRequestedPreDispatcher<,>));
        //services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(CustomerRequestedPostDispatcher<,>));

        var assembly = Assembly.GetExecutingAssembly();
        //var assembly2 = typeof(CreateCustomerCommand).GetTypeInfo().Assembly;
        //var assembly3 = AppDomain.CurrentDomain.GetAssemblies();
        return services.AddMediatR(assembly);
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        //services.AddScoped<IValidator<CreateCustomerDTO>>(x => 
        //    new CreateCustomerDTOValidator(x.GetRequiredService<ICustomerService>()));

        return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public static IApplicationBuilder IntializeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.EnsureCreated();

        //var dataInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
        //dataInitializer.InitializeDataAsync().GetAwaiter().GetResult();

        return app;
    }
}
