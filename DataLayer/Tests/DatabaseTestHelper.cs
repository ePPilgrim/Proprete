using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context;
using Proprette.DataLayer.Context.Configuration;
using Proprette.DataLayer.Entity.BasicData;
using Proprette.DataLayer.Entity.BasicData.Category;

namespace Proprette.DataLayer.Tests.DataLeyerTests;

internal static class DatabaseTestHelper
{
    public static PropretteDbContext CreatePropretteDbContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<PropretteDbContext>()
             //.UseSqlite("DataSource=:memory:")
            .UseMySql("server=localhost;user=root;password=1;database=DummyTestDB", new MariaDbServerVersion(new Version(10, 3, 29)))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .Options;
        var dbcontext = new PropretteDbContext(dbContextOptionsBuilder);
        return dbcontext;
    }

    public static bool EnsureDatabaseCreated(PropretteDbContext dbcontext)
    {
        //dbcontext.Database.OpenConnection();
        var res = dbcontext.Database.EnsureCreated();
        return res;
    }

    public static void EnsureDatabaseDeleted(PropretteDbContext dbcontext)
    {
        dbcontext.Database.EnsureDeleted();
        //dbcontext.Database.CloseConnection();
    }

    public static void SeedCategoryTables(PropretteDbContext dbcontext)
    {
        addCategory<Brand>(dbcontext);
        addCategory<Capacity>(dbcontext);
        addCategory<Color>(dbcontext);
        addCategory<Composition>(dbcontext);
        addCategory<FreeCode1>(dbcontext);
        addCategory<FreeCode2>(dbcontext);
        addCategory<FreeCode3>(dbcontext);
        addCategory<ItemType>(dbcontext);
        addCategory<Size>(dbcontext);
        addCategory<SubItem>(dbcontext);
        addCategory<Unit>(dbcontext);
        addCategory<Usage>(dbcontext);
        addCategory<Using>(dbcontext);
    }

    public static void SeedAddressTable(PropretteDbContext dbcontext)
    {
        var addresses = new List<Address>(){
           new Address() { Name = "Name1", City = "City1", Street = "Street1", Building = "Building1", ZipCode = "ZipCode" },
           new Address() { Name = "Name2", City = "City2", Street = "Street1", Building = "Building1", ZipCode = "ZipCode" },
           new Address() { Name = "Name3", City = "City2", Street = "Street1", Building = "Building3", ZipCode = "ZipCode" },
        };
        dbcontext.AddRange(addresses);
        dbcontext.SaveChanges();
    }

    public static void SeedUserTable(PropretteDbContext dbcontext)
    {
        var users = new List<User>(){
            new User() { Name = "Name1", FirstName = "FirstName1", LastName = "LastName1", Email = "Email1" },
            new User() { Name = "Name2", FirstName = "FirstName2", LastName = "LastName1", Email = "Email1" },
            new User() { Name = "Name3", FirstName = "FirstName2", LastName = "LastName2", Email = "Email1" },
        };
        dbcontext.AddRange(users);
        dbcontext.SaveChanges();
    }

    private static void addCategory<TEntity>(PropretteDbContext dbcontext) where TEntity : class, ICategory, new()
    {
        var categories = new List<TEntity>(){
            new TEntity() { Name = typeof(TEntity).Name },
            new TEntity() { Name = new string('x', ConfigurationHelper.MaxLengthOfCategoryName) }
        };
        dbcontext.AddRange(categories);
        dbcontext.SaveChanges();
    }
}
