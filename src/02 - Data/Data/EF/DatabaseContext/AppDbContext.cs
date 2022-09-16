using Data.EF.Extensions;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Data.EF.DatabaseContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (Database.IsInMemory())
            modelBuilder.ApplyAllConfigurationsForAppDbContext();
    }
}
