using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context;
using Proprette.DataLayer.Context.Configuration;
using Proprette.DataLayer.Entity.BasicData;
using Proprette.DataLayer.Entity.BasicData.Category;
using Proprette.DataLayer.Entity.Enums;
using Proprette.DataLayer.Entity.StaticData;

namespace Proprette.DataLayer.Tests.DataLeyerTests;

internal static class DatabaseTestHelper
{
    internal static PropretteDbContext CreatePropretteDbContext()
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

    internal static void EnsureDatabaseCreated(PropretteDbContext dbcontext)
    {
        //dbContext.Database.OpenConnection();
        if (dbcontext.Database.EnsureCreated()) return;    
        dbcontext.Database.EnsureDeleted();
        dbcontext.Database.EnsureCreated();
    }

    internal static void EnsureDatabaseDeleted(PropretteDbContext dbcontext)
    {
        dbcontext.Database.EnsureDeleted();
        //dbContext.Database.CloseConnection();
    }

    internal static void SeedCategoryTables(PropretteDbContext dbContext)
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
        dbContext.ChangeTracker.Clear();
    }

    internal static void SeedAddressTable(PropretteDbContext dbContext)
    {
        var addresses = new List<Address>(){
           new() { Name = "Name1", City = "City1", Street = "Street1", Building = "Building1", ZipCode = "ZipCode" },
           new() { Name = "Name2", City = "City2", Street = "Street1", Building = "Building1", ZipCode = "ZipCode" },
           new() { Name = "Name3", City = "City2", Street = "Street1", Building = "Building3", ZipCode = "ZipCode" },
        };
        dbContext.AddRange(addresses);
        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();
    }

    internal static void SeedUserTable(PropretteDbContext dbContext)
    {
        var users = new List<User>(){
            new User() { Name = "Name1", FirstName = "FirstName1", LastName = "LastName1", Email = "Email1" },
            new User() { Name = "Name2", FirstName = "FirstName2", LastName = "LastName1", Email = "Email1" },
            new User() { Name = "Name3", FirstName = "FirstName2", LastName = "LastName2", Email = "Email1" },
        };
        dbContext.AddRange(users);
        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();
    }

    internal static void SeedItemTable(PropretteDbContext dbContext)
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
        dbContext.ChangeTracker.Clear();
    }

    internal static void SeedWarehouseTable(PropretteDbContext dbContext)
    {
        SeedAddressTable(dbContext);
        var addresses = dbContext.Set<Address>().Select(a => a.Id).ToList();
        var warehouses = Enumerable.Range(0, addresses.Count).Select( i => new Warehouse() { Name = $"Warehouse{i}", AddressId = addresses[i]});
        dbContext.AddRange(warehouses);
        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();    
    }

    internal static void SeedHoldingTable(PropretteDbContext dbContext)
    {
        SeedItemTable(dbContext);
        SeedWarehouseTable(dbContext);
        var items = dbContext.Set<Item>().Select(i => i.Id).ToList();
        var warehouses = dbContext.Set<Warehouse>().Select(w => w.Id).ToList();
        var holdings = items.SelectMany(i => warehouses.Select(w => new Holding() { ItemId = i, WarehouseId = w }));
        dbContext.AddRange(holdings);
        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();
    }

    internal static void SeedTransactionTable(PropretteDbContext dbContext)
    {
        SeedHoldingTable(dbContext);
        SeedUserTable(dbContext);
        var holdings = dbContext.Set<Holding>().Select(h => h.Id).ToList();
        var users = dbContext.Set<User>().Select(u => u.Id).ToList();
        var dates = Enumerable.Range(0, 2).Select(i => new DateOnly(2021, 1, i + 1)).ToList();
        var transactionCodes = Enum.GetValues<TransactionCode>().Cast<TransactionCode>().ToList();
        var transactions = holdings.SelectMany(h =>
            users.SelectMany(u =>
                dates.SelectMany(d =>
                    transactionCodes.Select(tc =>
                        new Transaction()
                        {
                            HoldingId = h,
                            UserId = u,
                            Date = d,
                            TransactionCode = tc,
                            Nominal = 1,
                            Price = 1
                        }
                    )
                )
            )
        ).ToList();
        Enumerable.Range(0, transactions.Count).ToList().ForEach(i => transactions[i].Nominal = i); 
        Enumerable.Range(0, transactions.Count).ToList().ForEach(i => transactions[i].Price = (double)2*i);



        dbContext.AddRange(transactions);
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
