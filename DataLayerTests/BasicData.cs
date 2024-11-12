using Proprette.DataLayer.Entity.BasicData.Category;
using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context;
using Proprette.DataLayer.Context.Configuration;

namespace DataLayerTests;

[TestClass]
public class BasicData
{
    readonly private DbContextOptions<PropretteDbContext> dbContextOptionsBuilder = 
        new DbContextOptionsBuilder<PropretteDbContext>()
        .UseSqlite("DataSource=:memory:")
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        .Options;

    [TestMethod]
    public void RowsWithEmptyCategoryNamesAreInsertedAtDBCreation()
    {
        using var context= new PropretteDbContext(dbContextOptionsBuilder);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        var brand = context.Set<Brand>().Where(b => b.Id == ConfigurationHelper.IdOfEmptyCategoryName ).FirstOrDefault(); 
        var capacity = context.Set<Capacity>().Where(c => c.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var color = context.Set<Color>().Where(c => c.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var composition = context.Set<Composition>().Where(c => c.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var freeCode1 = context.Set<FreeCode1>().Where(f => f.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var freeCode2 = context.Set<FreeCode2>().Where(f => f.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var freeCode3 = context.Set<FreeCode3>().Where(f => f.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var itemType = context.Set<ItemType>().Where(i => i.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var size = context.Set<Size>().Where(s => s.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var subItem = context.Set<SubItem>().Where(s => s.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var unit = context.Set<Unit>().Where(u => u.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var usage = context.Set<Usage>().Where(u => u.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();
        var using_ = context.Set<Using>().Where(u => u.Id == ConfigurationHelper.IdOfEmptyCategoryName).FirstOrDefault();

        Assert.AreEqual(brand?.Name, string.Empty);
        Assert.AreEqual(capacity?.Name, string.Empty);
        Assert.AreEqual(color?.Name, string.Empty);
        Assert.AreEqual(composition?.Name, string.Empty);
        Assert.AreEqual(freeCode1?.Name, string.Empty);
        Assert.AreEqual(freeCode2?.Name, string.Empty);
        Assert.AreEqual(freeCode3?.Name, string.Empty);
        Assert.AreEqual(itemType?.Name, string.Empty);
        Assert.AreEqual(size?.Name, string.Empty);
        Assert.AreEqual(subItem?.Name, string.Empty);
        Assert.AreEqual(unit?.Name, string.Empty);
        Assert.AreEqual(usage?.Name, string.Empty);
        Assert.AreEqual(using_?.Name, string.Empty);

        context.Database.CloseConnection();
        context.Database.EnsureDeleted();
    }
}