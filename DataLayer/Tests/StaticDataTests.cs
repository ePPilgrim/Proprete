using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context.Configuration;
using Proprette.DataLayer.Entity.BasicData.Category;
using Proprette.DataLayer.Entity.StaticData;
using Proprette.DataLayer.Tests.DataLeyerTests;

namespace Proprette.DataLayer.Tests.DataLayerTests;

[TestClass]
public class StaticDataTests
{
    [TestMethod]
    public void ItemTableShouldNotContainZeroRowsByDefault()
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
    public void ItemTableShouldHaveUniqueIndexOnAllCategoryFields()
    {
        // Arrange
        var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedItemTable(dbContext);
        var record = dbContext.Set<Item>().Where(x => x.Name == "Item").FirstOrDefault();
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
    public void ItemTableShouldAllowMultipleRowsWithSameNameField()
    {
        // Arrange
        var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
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
    public void ItemTableShouldAllowRowWithEmptyCategoryFields()
    {
        // Arrange
        var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
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
    public void ItemTableShouldAllowNewRecordInsertion()
    {
        // Arrange
        var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
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
}
