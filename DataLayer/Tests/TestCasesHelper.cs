using Proprette.DataLayer.Context;
using Proprette.DataLayer.Context.Configuration;
using Proprette.DataLayer.Entity.BasicData.Category;

namespace Proprette.DataLayer.Tests;

internal static class TestCasesHelper
{
    public static bool InitialTableCreation(PropretteDbContext dbcontext)
    {
        //dbcontext.Database.OpenConnection();
        var res = dbcontext.Database.EnsureCreated();
        return res;
    }

    public static void DisposeTable(PropretteDbContext dbcontext)
    {
        dbcontext.Database.EnsureDeleted();
        //dbcontext.Database.CloseConnection();
    }

    public static void PopulateCategoryTables(PropretteDbContext dbcontext)
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
