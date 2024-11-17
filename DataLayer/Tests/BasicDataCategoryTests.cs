using Proprette.DataLayer.Entity.BasicData.Category;
using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Context.Configuration;

namespace Proprette.DataLayer.Tests.DataLeyerTests;

[TestClass]
public class BasicDataCategoryTests
{
    [TestMethod]
    public void DefaultCategoryTablesContainOnlyZeroRows()
    {
        using var context= DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(context);

        var brand = context.Set<Brand>(); 
        var capacity = context.Set<Capacity>();
        var color = context.Set<Color>();
        var composition = context.Set<Composition>();
        var freeCode1 = context.Set<FreeCode1>();
        var freeCode2 = context.Set<FreeCode2>();
        var freeCode3 = context.Set<FreeCode3>();
        var itemType = context.Set<ItemType>();
        var size = context.Set<Size>();
        var subItem = context.Set<SubItem>();
        var unit = context.Set<Unit>();
        var usage = context.Set<Usage>();

        Assert.AreEqual(brand.Count(), 1);
        Assert.AreEqual(capacity.Count(), 1);
        Assert.AreEqual(color.Count(), 1);
        Assert.AreEqual(composition.Count(), 1);
        Assert.AreEqual(freeCode1.Count(), 1);
        Assert.AreEqual(freeCode2.Count(), 1);
        Assert.AreEqual(freeCode3.Count(), 1);
        Assert.AreEqual(itemType.Count(), 1);
        Assert.AreEqual(size.Count(), 1);
        Assert.AreEqual(subItem.Count(), 1);
        Assert.AreEqual(unit.Count(), 1);
        Assert.AreEqual(usage.Count(), 1);

        Assert.AreEqual(brand?.First().Name, string.Empty);
        Assert.AreEqual(capacity?.First().Name, string.Empty);
        Assert.AreEqual(color?.First().Name, string.Empty);
        Assert.AreEqual(composition?.First().Name, string.Empty);
        Assert.AreEqual(freeCode1?.First().Name, string.Empty);
        Assert.AreEqual(freeCode2?.First().Name, string.Empty);
        Assert.AreEqual(freeCode3?.First().Name, string.Empty);
        Assert.AreEqual(itemType?.First().Name, string.Empty);
        Assert.AreEqual(size?.First().Name, string.Empty);
        Assert.AreEqual(subItem?.First().Name, string.Empty);
        Assert.AreEqual(unit?.First().Name, string.Empty);
        Assert.AreEqual(usage?.First().Name, string.Empty);

        Assert.AreEqual(brand?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(capacity?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(color?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(composition?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(freeCode1?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(freeCode2?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(freeCode3?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(itemType?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(size?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(subItem?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(unit?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);
        Assert.AreEqual(usage?.First().Id, ConfigurationHelper.IdOfEmptyCategoryName);

        DatabaseTestHelper.EnsureDatabaseDeleted(context);
    }

    [TestMethod]
    public void CanInsertNewCategory()
    {
        using var context = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(context);
        DatabaseTestHelper.SeedCategoryTables(context);

        Assert.IsTrue(sequenceEqual(context.Set<Brand>()));
        Assert.IsTrue(sequenceEqual(context.Set<Capacity>()));
        Assert.IsTrue(sequenceEqual(context.Set<Color>()));
        Assert.IsTrue(sequenceEqual(context.Set<Composition>()));
        Assert.IsTrue(sequenceEqual(context.Set<FreeCode1>()));
        Assert.IsTrue(sequenceEqual(context.Set<FreeCode2>()));
        Assert.IsTrue(sequenceEqual(context.Set<FreeCode3>()));
        Assert.IsTrue(sequenceEqual(context.Set<ItemType>()));
        Assert.IsTrue(sequenceEqual(context.Set<Size>()));
        Assert.IsTrue(sequenceEqual(context.Set<SubItem>()));
        Assert.IsTrue(sequenceEqual(context.Set<Unit>()));
        Assert.IsTrue(sequenceEqual(context.Set<Usage>()));

        DatabaseTestHelper.EnsureDatabaseDeleted(context);
    }

#region CannotInsertDuplicateNames
        [TestMethod]
    public void CannotInsertDuplicateNamesOfBrand() => template_CannotInsertDuplicateNames<Brand>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfCapacity() => template_CannotInsertDuplicateNames<Capacity>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfColor() => template_CannotInsertDuplicateNames<Color>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfComposition() => template_CannotInsertDuplicateNames<Composition>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfFreeCode1() => template_CannotInsertDuplicateNames<FreeCode1>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfFreeCode2() => template_CannotInsertDuplicateNames<FreeCode2>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfFreeCode3() => template_CannotInsertDuplicateNames<FreeCode3>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfItemType() => template_CannotInsertDuplicateNames<ItemType>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfSize() => template_CannotInsertDuplicateNames<Size>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfSubItem() => template_CannotInsertDuplicateNames<SubItem>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfUnit() => template_CannotInsertDuplicateNames<Unit>();

    [TestMethod]
    public void CannotInsertDuplicateNamesOfUsage() => template_CannotInsertDuplicateNames<Usage>();
    #endregion

#region NameExceedsMaxLengthThrowsException
    [TestMethod]
    public void BrandNameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<Brand>();

    [TestMethod]
    public void CapacityNameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<Capacity>();

    [TestMethod]
    public void ColorNameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<Color>();

    [TestMethod]
    public void CompositionNameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<Composition>();

    [TestMethod]
    public void FreeCode1NameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<FreeCode1>();

    [TestMethod]
    public void FreeCode2NameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<FreeCode2>();

    [TestMethod]
    public void FreeCode3NameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<FreeCode3>();

    [TestMethod]
    public void ItemTypeNameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<ItemType>();

    [TestMethod]
    public void SizeNameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<Size>();

    [TestMethod]
    public void SubItemNameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<SubItem>();

    [TestMethod]
    public void UnitNameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<Unit>();

    [TestMethod]
    public void UsageNameExceedsMaxLengthThrowsException() => template_NameExceedsMaxLengthThrowsException<Usage>();
    #endregion

    private void template_NameExceedsMaxLengthThrowsException<TEntity>() where TEntity: ICategory, new ()
    {
        using var context = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(context);

        Assert.ThrowsException<DbUpdateException>(() =>
        {
            context.Add(new TEntity() { Name = "123456789012345678901234567890123" });
            context.SaveChanges();
        });

        DatabaseTestHelper.EnsureDatabaseDeleted(context);
    }

    private void template_CannotInsertDuplicateNames<TEntity>() where TEntity: ICategory, new()
    {
        using var context = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(context);
        context.Add(new TEntity() { Name = typeof(TEntity).Name });
        context.SaveChanges();
        context.ChangeTracker.Clear();

        Assert.ThrowsException<DbUpdateException>(() =>
        {
            context.Add(new TEntity() { Name = typeof(TEntity).Name });
            context.SaveChanges();
        });

        DatabaseTestHelper.EnsureDatabaseDeleted(context);
    }

    private bool sequenceEqual<TEntity>(IEnumerable<TEntity> rows) where TEntity : ICategory, new()
    {
        var names = rows.Select(x => x.Name).ToArray();
        if(names.Length != 3) return false;
        var expectedNames = new HashSet<string>() { 
            string.Empty, 
            typeof(TEntity).Name, 
            new string('x', ConfigurationHelper.MaxLengthOfCategoryName) };
        return names.All(x => expectedNames.Contains(x));
    }

}