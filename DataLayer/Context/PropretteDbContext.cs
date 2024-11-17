using Proprette.DataLayer.Entity.BasicData.Category;
using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context.Configuration;
using System.Reflection;

namespace Proprette.DataLayer.Context;

public class PropretteDbContext : DbContext
{
    public PropretteDbContext(DbContextOptions<PropretteDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<Brand>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<Capacity>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<Color>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<Composition>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<FreeCode1>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<FreeCode2>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<FreeCode3>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<ItemType>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<Size>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<SubItem>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<Unit>());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration<Usage>());
    }
}