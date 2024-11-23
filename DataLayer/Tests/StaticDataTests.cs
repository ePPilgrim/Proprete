using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context.Configuration;
using Proprette.DataLayer.Entity.BasicData.Category;
using Proprette.DataLayer.Entity.StaticData;

namespace Proprette.DataLayer.Tests.DataLayerTests;

[TestClass]
public class StaticDataTests
{
#region Item Table Tests
    [TestMethod]
    public void ItemTable_ShouldNotContainZeroRowsByDefault()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        // Act
        var actualItems = dbContext.Set<Item>().ToList();
        // Assert
        Assert.AreEqual(0, actualItems.Count);
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void ItemTable_ShouldHaveUniqueIndexOnAllCategoryFields()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedItemTable(dbContext);
        var record = dbContext.Set<Item>().FirstOrDefault(x => x.Name == "Item");
        dbContext.ChangeTracker.Clear();
        // Act
        Assert.IsNotNull(record);
        record.Name = "NewItem";
        dbContext.Add(record);
        // Assert
        Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void ItemTable_ShouldAllowMultipleRowsWithSameNameField()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedItemTable(dbContext);
        var newRecord = new Item
        {
            Name = "Item",
            Brand = new Brand { Name = "NewBrand" },
            ItemType = new ItemType { Name = "NewItemType" },
            Usage = new Usage { Name = "NewUsage" },
            Color = new Color { Name = "NewColor" },
            Capacity = new Capacity { Name = "NewCapacity" },
            Size = new Size { Name = "NewSize" },
            Unit = new Unit { Name = "NewUnit" },
            SubItem = new SubItem { Name = "NewSubItem" },
            Composition = new Composition { Name = "NewComposition" },
            FreeCode1 = new FreeCode1 { Name = "NewFreeCode1" },
            FreeCode2 = new FreeCode2 { Name = "NewFreeCode2" },
            FreeCode3 = new FreeCode3 { Name = "NewFreeCode3" }
        };
        // Act
        dbContext.Add(newRecord);
        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();
        var actualRecords = dbContext.Set<Item>().Where(item => item.Name == "Item").ToList();
        // Assert
        Assert.AreEqual(2, actualRecords.Count);
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void ItemTable_ShouldAllowRowWithEmptyCategoryFields()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedItemTable(dbContext);
        // Act
        var actualRows = dbContext.Set<Item>().Where(x =>
            x.BrandId == ConfigurationHelper.IdOfEmptyCategoryName
            && x.ItemTypeId == ConfigurationHelper.IdOfEmptyCategoryName
            && x.UsageId == ConfigurationHelper.IdOfEmptyCategoryName
            && x.ColorId == ConfigurationHelper.IdOfEmptyCategoryName
            && x.CapacityId == ConfigurationHelper.IdOfEmptyCategoryName
            && x.SizeId == ConfigurationHelper.IdOfEmptyCategoryName
            && x.UnitId == ConfigurationHelper.IdOfEmptyCategoryName
            && x.SubItemId == ConfigurationHelper.IdOfEmptyCategoryName
            && x.CompositionId == ConfigurationHelper.IdOfEmptyCategoryName
            && x.FreeCode1Id == ConfigurationHelper.IdOfEmptyCategoryName
            && x.FreeCode2Id == ConfigurationHelper.IdOfEmptyCategoryName
            && x.FreeCode3Id == ConfigurationHelper.IdOfEmptyCategoryName)
            .Include(x => x.Brand)
            .Include(x => x.ItemType)
            .Include(x => x.Usage)
            .Include(x => x.Color)
            .Include(x => x.Capacity)
            .Include(x => x.Size)
            .Include(x => x.Unit)
            .Include(x => x.SubItem)
            .Include(x => x.Composition)
            .Include(x => x.FreeCode1)
            .Include(x => x.FreeCode2)
            .Include(x => x.FreeCode3)
            .Select(x => x.Brand.Name
            + x.ItemType.Name
            + x.Usage.Name
            + x.Color.Name
            + x.Capacity.Name
            + x.Size.Name
            + x.Unit.Name
            + x.SubItem.Name
            + x.Composition.Name
            + x.FreeCode1.Name
            + x.FreeCode2.Name
            + x.FreeCode3.Name)
            .ToList();
        // Assert
        Assert.AreEqual(1, actualRows.Count);
        Assert.AreEqual(string.Empty, actualRows[0]);
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void ItemTable_ShouldAllowNewRecordInsertion()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedItemTable(dbContext);
        dbContext.ChangeTracker.Clear();
        // Act
        var actualResult = dbContext.Set<Item>()
            .Select(item => item.Name
                + item.Brand.Name
                + item.ItemType.Name
                + item.Usage.Name
                + item.Color.Name
                + item.Capacity.Name
                + item.Size.Name
                + item.Unit.Name
                + item.SubItem.Name
                + item.Composition.Name
                + item.FreeCode1.Name
                + item.FreeCode2.Name
                + item.FreeCode3.Name)
            .ToList();
        // Assert
        Assert.IsTrue(actualResult.Count == 3);
        Assert.IsTrue(actualResult.Contains("ItemBrandItemTypeUsageColorCapacitySizeUnitSubItemCompositionFreeCode1FreeCode2FreeCode3"));
        Assert.IsTrue(actualResult.Contains("EmptyCategories"));
        Assert.IsTrue(actualResult.Contains("BrandUsageCapacityUnitCompositionFreeCode2"));
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }
    #endregion

#region Warehouse Table Tests
    [TestMethod]
    public void WarehouseTable_ShouldBeEmptyInitially()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        // Act
        var actualWarehouses = dbContext.Set<Warehouse>().ToList();
        // Assert
        Assert.AreEqual(0, actualWarehouses.Count);
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void WarehouseTable_ShouldHaveUniqueNameField()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedWarehouseTable(dbContext);
        var record = dbContext.Set<Warehouse>().FirstOrDefault(w => w.Name == "Warehouse0");
        dbContext.ChangeTracker.Clear();
        // Act
        Assert.IsNotNull(record);
        record.Name = "Warehouse1";
        dbContext.Add(record);
        // Assert
        Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void WarehouseTable_ShouldHaveUniqueIndexOnAddressIdField()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedWarehouseTable(dbContext);
        var record = dbContext.Set<Warehouse>().FirstOrDefault();
        dbContext.ChangeTracker.Clear();
        // Act
        Assert.IsNotNull(record);
        record.Name = "NewWarehouse";
        dbContext.Add(record);
        // Assert
        Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void WarehouseTable_ShouldAllowNewRecordInsertion()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedWarehouseTable(dbContext);
        dbContext.ChangeTracker.Clear();
        // Act
        var actualResult = dbContext.Set<Warehouse>().Select(warehouse => warehouse.Name).ToList();
        // Assert
        Assert.IsTrue(actualResult.Count == 3);
        Assert.AreEqual("Warehouse0", actualResult[0]);
        Assert.AreEqual("Warehouse1", actualResult[1]);
        Assert.AreEqual("Warehouse2", actualResult[2]);
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }
    #endregion

#region Holding Table Tests
    [TestMethod]
    public void HoldingTable_ShouldNotContainZeroRowsByDefault()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        // Act
        var actualHoldings = dbContext.Set<Holding>().ToList();
        // Assert
        Assert.AreEqual(0, actualHoldings.Count);
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void HoldingTable_ShouldHaveUniqueIndexOnItemIdAndWarehouseIdFields()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedHoldingTable(dbContext);
        var record = dbContext.Set<Holding>().FirstOrDefault();
        dbContext.ChangeTracker.Clear();
        // Act
        Assert.IsNotNull(record);
        record.Id = 0;
        dbContext.Add(record);
        // Assert
        Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void HoldingTable_ShouldAllowNewRecordInsertion()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedHoldingTable(dbContext);
        dbContext.ChangeTracker.Clear();
        // Act
        var actualResult = dbContext.Set<Holding>()
            .Include(h => h.Item)
            .Include(h => h.Warehouse)
            .Select(h => h.Item.Name + h.Warehouse.Name)
            .ToList();
        // Assert
        Assert.IsTrue(actualResult.Count == 9);
        Assert.IsTrue(actualResult.Contains("ItemWarehouse0"));
        Assert.IsTrue(actualResult.Contains("ItemWarehouse1"));
        Assert.IsTrue(actualResult.Contains("ItemWarehouse2"));
        Assert.IsTrue(actualResult.Contains("EmptyCategoriesWarehouse0"));
        Assert.IsTrue(actualResult.Contains("EmptyCategoriesWarehouse1"));
        Assert.IsTrue(actualResult.Contains("EmptyCategoriesWarehouse2"));
        Assert.IsTrue(actualResult.Contains("Warehouse0"));
        Assert.IsTrue(actualResult.Contains("Warehouse1"));
        Assert.IsTrue(actualResult.Contains("Warehouse2"));
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }
    #endregion

#region Transaction Table Tests
    [TestMethod]
    public void TransactionTable_ShouldNotContainZeroRowsByDefault()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        // Act
        var actualTransactions = dbContext.Set<Transaction>().ToList();
        // Assert
        Assert.AreEqual(0, actualTransactions.Count);
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void TransactionTable_ShouldAllowNewRecordInsertion()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedTransactionTable(dbContext);
        // Act
        var actualResult = dbContext.Set<Transaction>()
            .Include(t => t.Holding)
            .Include(t => t.User)
            .Select(t => t.Holding.Item.Name + t.User.Name + t.Date + t.TransactionCode + t.Nominal + t.Price)
            .ToList();

        // Assert
        Assert.IsTrue(actualResult.Count == 270);
        Assert.IsTrue(actualResult.Contains("EmptyCategoriesName12021-01-01000"));
        Assert.IsTrue(actualResult.Contains("EmptyCategoriesName32021-01-0242958"));
        Assert.IsTrue(actualResult.Contains("EmptyCategoriesName22021-01-023198396"));
        Assert.IsTrue(actualResult.Contains("ItemName12021-01-0113162"));
        Assert.IsTrue(actualResult.Contains("ItemName22021-01-0224794"));
        Assert.IsTrue(actualResult.Contains("ItemName32021-01-013143286"));
        Assert.IsTrue(actualResult.Contains("Name12021-01-01060120"));
        Assert.IsTrue(actualResult.Contains("Name22021-01-02277154"));
        Assert.IsTrue(actualResult.Contains("Name32021-01-01484168"));
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void TransactionTable_ShouldHaveTimeStampFieldWithDefaultValue()
    {
        // Arrange
        var rnd = new Random();
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedTransactionTable(dbContext);
        // Act
        var timeSeries = dbContext.Set<Transaction>()
            .AsTracking()
            .Select(x => new { x.Id, TimeStamp = EF.Property<DateTime>(x, "TimeStamp").Ticks })
            .ToList()
            .OrderBy(x => rnd.Next());
        var sortedByTimeStamp = timeSeries.OrderBy(x => x.TimeStamp).ToList();
        var sortedById = timeSeries.OrderBy(x => x.Id).ToList();
        // Assert
        Assert.IsTrue(sortedById.SequenceEqual(sortedByTimeStamp));
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }
#endregion
}
