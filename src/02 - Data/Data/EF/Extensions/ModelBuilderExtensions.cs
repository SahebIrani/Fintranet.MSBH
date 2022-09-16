using Data.EF.Mappings;

using Microsoft.EntityFrameworkCore;

namespace Data.EF.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAllConfigurationsForAppDbContext(this ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema("MSBH");
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        //modelBuilder.ApplyAllConfigurationsForAppDbContext();
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext)));
    }
}
