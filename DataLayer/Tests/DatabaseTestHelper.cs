using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context;
using Proprette.DataLayer.Context.Configuration;
using Proprette.DataLayer.Entity.BasicData;
using Proprette.DataLayer.Entity.BasicData.Category;
using Proprette.DataLayer.Entity.StaticData;

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
        var dbContext = new PropretteDbContext(dbContextOptionsBuilder);
        dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        return dbContext;
    }

    public static void EnsureDatabaseCreated(PropretteDbContext dbcontext)
    {
        //dbcontext.Database.OpenConnection();
        if (dbcontext.Database.EnsureCreated()) return;    
        dbcontext.Database.EnsureDeleted();
        dbcontext.Database.EnsureCreated();
    }

    public static void EnsureDatabaseDeleted(PropretteDbContext dbcontext)
    {
        dbcontext.Database.EnsureDeleted();
        //dbcontext.Database.CloseConnection();
    }

    public static void SeedCategoryTables(PropretteDbContext dbContext)
    {
        addCategory<Brand>(dbContext);
        addCategory<Capacity>(dbContext);
        addCategory<Color>(dbContext);
        addCategory<Composition>(dbContext);
        addCategory<FreeCode1>(dbContext);
        addCategory<FreeCode2>(dbContext);
        addCategory<FreeCode3>(dbContext);
        addCategory<ItemType>(dbContext);
        addCategory<Size>(dbContext);
        addCategory<SubItem>(dbContext);
        addCategory<Unit>(dbContext);
        addCategory<Usage>(dbContext);
    }

    public static void SeedAddressTable(PropretteDbContext dbcontext)
    {
        var addresses = new List<Address>(){
           new() { Name = "Name1", City = "City1", Street = "Street1", Building = "Building1", ZipCode = "ZipCode" },
           new() { Name = "Name2", City = "City2", Street = "Street1", Building = "Building1", ZipCode = "ZipCode" },
           new() { Name = "Name3", City = "City2", Street = "Street1", Building = "Building3", ZipCode = "ZipCode" },
        };
        dbcontext.AddRange(addresses);
        dbcontext.SaveChanges();
    }

    public static void SeedUserTable(PropretteDbContext dbContext)
    {
        var users = new List<User>(){
            new User() { Name = "Name1", FirstName = "FirstName1", LastName = "LastName1", Email = "Email1" },
            new User() { Name = "Name2", FirstName = "FirstName2", LastName = "LastName1", Email = "Email1" },
            new User() { Name = "Name3", FirstName = "FirstName2", LastName = "LastName2", Email = "Email1" },
        };
        dbContext.AddRange(users);
        dbContext.SaveChanges();
    }

    public static void SeedItemTable(PropretteDbContext dbContext)
    {
        var rows = new List<Item>() {
            new()
            {
                Name = "Item",
                Brand = new Brand() { Name = "Brand" },
                ItemType = new ItemType() { Name = "ItemType" },
                Usage = new Usage() { Name = "Usage" },
                Color = new Color() { Name = "Color" },
                Capacity = new Capacity() { Name = "Capacity" },
                Size = new Size() { Name = "Size" },
                Unit = new Unit() { Name = "Unit" },
                SubItem = new SubItem() { Name = "SubItem" },
                Composition = new Composition() { Name = "Composition" },
                FreeCode1 = new FreeCode1() { Name = "FreeCode1" },
                FreeCode2 = new FreeCode2() { Name = "FreeCode2" },
                FreeCode3 = new FreeCode3() { Name = "FreeCode3" }
            },
            new()
            { 
                Name = "EmptyCategories",
                BrandId = ConfigurationHelper.IdOfEmptyCategoryName,
                ItemTypeId = ConfigurationHelper.IdOfEmptyCategoryName,
                UsageId = ConfigurationHelper.IdOfEmptyCategoryName,
                ColorId = ConfigurationHelper.IdOfEmptyCategoryName,
                CapacityId = ConfigurationHelper.IdOfEmptyCategoryName,
                SizeId = ConfigurationHelper.IdOfEmptyCategoryName,
                UnitId = ConfigurationHelper.IdOfEmptyCategoryName,
                SubItemId = ConfigurationHelper.IdOfEmptyCategoryName,
                CompositionId = ConfigurationHelper.IdOfEmptyCategoryName,
                FreeCode1Id = ConfigurationHelper.IdOfEmptyCategoryName,
                FreeCode2Id = ConfigurationHelper.IdOfEmptyCategoryName,
                FreeCode3Id = ConfigurationHelper.IdOfEmptyCategoryName
            }
        };

        dbContext.AddRange(rows);
        dbContext.SaveChanges();

        var nonEmptyRow = dbContext.Set<Item>().Where(x => x.Name == "Item").FirstOrDefault();

        if(nonEmptyRow == null) return;

        dbContext.Add(new Item()
        {
            Name = string.Empty,
            BrandId = nonEmptyRow.BrandId,
            ItemTypeId = ConfigurationHelper.IdOfEmptyCategoryName,
            UsageId = nonEmptyRow.UsageId,
            ColorId = ConfigurationHelper.IdOfEmptyCategoryName,
            CapacityId = nonEmptyRow.CapacityId,
            SizeId = ConfigurationHelper.IdOfEmptyCategoryName,
            UnitId = nonEmptyRow.UnitId,
            SubItemId = ConfigurationHelper.IdOfEmptyCategoryName,
            CompositionId = nonEmptyRow.CompositionId,
            FreeCode1Id = ConfigurationHelper.IdOfEmptyCategoryName,
            FreeCode2Id = nonEmptyRow.FreeCode2Id,
            FreeCode3Id = ConfigurationHelper.IdOfEmptyCategoryName
        });
        dbContext.SaveChanges();
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
