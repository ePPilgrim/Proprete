
using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context.Configuration;
using Proprette.DataLayer.Entity.Category;
using System.Reflection;

namespace Proprette.DataLayer.Context;

public class PropretteDbContext : DbContext
{
   // public PropretteDbContext(DbContextOptions<PropretteDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "server=localhost;user=root;password=1;database=newproprettedb";
        optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 3, 29)));
                //mySqlOptions => mySqlOptions.MigrationsAssembly(
                //    typeof(ServiceRegistration).Assembly.FullName));
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //var interfaceCategoryType = typeof(ICategory);
        //var entityAssembly = interfaceCategoryType.Assembly;
        //var allCategoryTypes = interfaceCategoryType.Assembly.GetTypes()
        //                .Where(type => interfaceCategoryType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);
        //foreach(var type in allCategoryTypes)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(type.Assembly);
        //}
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //modelBuilder.Entity<Brand>().HasData(new Brand(string.Empty) { Id = -1});
        //modelBuilder.Entity<Color>().HasData(new Color(string.Empty) { Id = -1});
    }
}