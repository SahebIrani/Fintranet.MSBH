using Data.EF.DatabaseContext;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Xunit;

namespace Test.Integration;

public class WebApplicationFactoryWithInMemory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(_ =>
                    _.ServiceType == typeof(DbContextOptions<AppDbContext>)
                )
            ;

            if (descriptor != null)
                services.Remove(descriptor);

            string connectionString = new DatabaseConfiguration().GetDataConnectionString();

            services.AddDbContextPool<AppDbContext>(_ => _.UseInMemoryDatabase(connectionString));

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();

            var scopedServices = scope.ServiceProvider;

            using var appContext = scopedServices.GetRequiredService<AppDbContext>();

            var logger = scopedServices.GetRequiredService<ILogger<WebApplicationFactoryWithInMemory<TEntryPoint>>>();

            try
            {
                //if (appContext.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory"))
                appContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                    "database with test messages. Error: {Message}", ex.Message);
            }
        });
    }
}

public class CustomersControllerIntegrationTests : IClassFixture<WebApplicationFactoryWithInMemory<Program>>
{
    private readonly HttpClient _client;

    private readonly HttpClient _httpClient;

    private readonly WebApplicationFactoryWithInMemory<Program> _factory;

    public CustomersControllerIntegrationTests(WebApplicationFactoryWithInMemory<Program> factory)
    {
        _factory = factory;

        var clientOptions = new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true,
            BaseAddress = new Uri("https://localhost:7085"),
            HandleCookies = true,
            MaxAutomaticRedirections = 7
        };

        _client = _factory.CreateClient(clientOptions);

        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<AppDbContext>();
            });
        })
            .CreateClient(clientOptions)
        ;

        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }

    [Fact]
    public async Task Index_WhenCalled_ReturnsApplicationForm()
    {
        var response = await _httpClient.GetAsync("/CustomersContoller/GetAll");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Contains("Sinjul", responseString);
        Assert.Contains("MSBH", responseString);
    }

    [Fact]
    public async Task Create_WhenPOSTExecuted_ReturnsToApiResultCustomer()
    {
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/CustomersContoller/Add");

        var formModel = new Dictionary<string, string>
        {
            { "firstName", "Jack".ToUpper() },
            { "lastname", "Slater".ToUpper() },
            { "dateOfBirth", "2022-05-26T23:31:26.989Z" },
            { "phoneNumber", "+44 117 496 0123" },
            { "countryCodeSelected", "US" },
            { "email", "jackslater.irani@gmail.com" },
            { "bankAccountNumber", "1234" },
        };

        postRequest.Content = new FormUrlEncodedContent(formModel);

        var response = await _client.SendAsync(postRequest);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Contains("Jack", responseString);
        Assert.Contains("117 496 0123", responseString);
    }
}
