using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Entity.ReportData;

namespace Proprette.DataLayer.Tests.DataLayerTests;

[TestClass]
public class ReportDataTests
{
    [TestMethod]
    public void PositionTable_ShouldNotContainZeroRowsByDefault()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        // Act
        var actualPositions = dbContext.Set<Position>().ToList();
        // Assert
        Assert.AreEqual(0, actualPositions.Count);
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void PositionTable_ShouldHaveAlternateKeyOnHoldingIdAndDate()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedPositionTable(dbContext);
        var record = dbContext.Set<Position>().FirstOrDefault();
        dbContext.ChangeTracker.Clear();
        // Act
        Assert.IsNotNull(record);
        record.Id = 0;
        record.BalanceNominal++;
        dbContext.Add(record);
        // Assert
        Assert.ThrowsException<DbUpdateException>(() => dbContext.SaveChanges());
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void PositionTable_ShouldAllowNewRecordInsertion()
    {
        // Arrange
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedPositionTable(dbContext);
        dbContext.ChangeTracker.Clear();
        // Act
        var actualPositions = dbContext.Set<Position>()
            .Include(p => p.Holding)
            .Select( p => p.Holding.Item.Name + p.Holding.Warehouse.Name + p.Date + p.BalanceNominal)
            .ToList();
        // Assert
        Assert.AreEqual(27, actualPositions.Count);
        Assert.IsTrue(actualPositions.Contains("EmptyCategoriesWarehouse02021-01-011"));
        Assert.IsTrue(actualPositions.Contains("EmptyCategoriesWarehouse22023-03-0321"));
        Assert.IsTrue(actualPositions.Contains("Warehouse02022-02-028"));
        Assert.IsTrue(actualPositions.Contains("Warehouse12022-02-0217"));
        Assert.IsTrue(actualPositions.Contains("ItemWarehouse02023-03-036"));
        Assert.IsTrue(actualPositions.Contains("ItemWarehouse22021-01-0122"));
        DatabaseTestHelper.EnsureDatabaseDeleted(dbContext);
    }

    [TestMethod]
    public void PositionTable_ShouldHaveTimeStampFieldWithDefaultValue()
    {
        // Arrange
        var rnd = new Random();
        using var dbContext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbContext);
        DatabaseTestHelper.SeedTransactionTable(dbContext);
        // Act
        var timeSeries = dbContext.Set<Position>()
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
}
