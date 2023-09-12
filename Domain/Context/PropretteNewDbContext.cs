using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Context.Configuration.Category;
using Proprette.Domain.Data.Entities.Category;

namespace Proprette.Domain.Context;

public class PropretteNewDbContext : DbContext
{
    public PropretteNewDbContext(DbContextOptions<PropretteNewDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration<Brand>).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration<Color>).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NewItemConfiguration).Assembly);
    }
}