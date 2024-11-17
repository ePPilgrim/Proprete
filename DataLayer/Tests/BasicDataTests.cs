using Microsoft.EntityFrameworkCore;
using Proprette.DataLayer.Entity.BasicData;

namespace Proprette.DataLayer.Tests.DataLeyerTests;

[TestClass]
public class BasicDataTests
{

#region AddressTable
    [TestMethod]
    public void AddressTableShouldNotContainZeroRowsByDefault()
    {
        using var dbcontext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbcontext);

        var addresses = dbcontext.Set<Address>().ToList();
        Assert.AreEqual(0, addresses.Count);

        DatabaseTestHelper.EnsureDatabaseDeleted(dbcontext);
    }

    [TestMethod]
    public void AddressTableShouldHaveUniqueIndexOnCityStreetBuilding()
    {
        using var dbcontext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbcontext);

        var address1 = new Address()
        {
            Name = "Name1",
            City = "City1",
            Street = "Street1",
            Building = "Building1",
            ZipCode = "ZipCode1"
        };
        dbcontext.Add(address1);
        dbcontext.SaveChanges();
        dbcontext.ChangeTracker.Clear();
        var address2 = new Address()
        {
            Name = "Name2",
            City = "City1",
            Street = "Street1",
            Building = "Building1",
            ZipCode = "ZipCode2"
        };
        dbcontext.Add(address2);

        Assert.ThrowsException<DbUpdateException>(() => dbcontext.SaveChanges());

        DatabaseTestHelper.EnsureDatabaseDeleted(dbcontext);
    }

    [TestMethod] 
    public void AddressTableShouldHaveAlternateKeyOnName()
    {
        using var dbcontext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbcontext);
        var address1 = new Address()
        {
            Name = "Name1",
            City = "City1",
            Street = "Street1",
            Building = "Building1",
            ZipCode = "ZipCode1"
        };
        dbcontext.Add(address1);
        dbcontext.SaveChanges();
        dbcontext.ChangeTracker.Clear();
        var address2 = new Address()
        {
            Name = "Name1",
            City = "City2",
            Street = "Street2",
            Building = "Building2",
            ZipCode = "ZipCode2"
        };
        dbcontext.Add(address2);
        Assert.ThrowsException<DbUpdateException>(() => dbcontext.SaveChanges());

        DatabaseTestHelper.EnsureDatabaseDeleted(dbcontext);
    }

    [TestMethod]
    public void AddressTableCanInsertThreeRowsWithDifferentAddresses()
    {
        using var dbcontext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbcontext);
        DatabaseTestHelper.SeedAddressTable(dbcontext);

        var addresses = dbcontext.Set<Address>().Select(a => a.Name + a.City + a.Street + a.Building + a.ZipCode).ToList();

        Assert.AreEqual(3, addresses.Count);
        Assert.IsTrue(addresses.Contains("Name1City1Street1Building1ZipCode"));
        Assert.IsTrue(addresses.Contains("Name2City2Street1Building1ZipCode"));
        Assert.IsTrue(addresses.Contains("Name3City2Street1Building3ZipCode"));

        DatabaseTestHelper.EnsureDatabaseDeleted(dbcontext);
    }
    #endregion

    #region UserTable
    [TestMethod]
    public void UserTableShouldNotContainZeroRowsByDefault()
    {
        using var dbcontext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbcontext);

        var users = dbcontext.Set<User>().ToList();

        Assert.AreEqual(0, users.Count);

        DatabaseTestHelper.EnsureDatabaseDeleted(dbcontext);
    }

    [TestMethod]
    public void UserTableShouldHaveUniqueIndexOnFirstNameLastNameEmail()
    {
        using var dbcontext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbcontext);

        var user1 = new User()
        {
            Name = "Name1",
            FirstName = "FirstName1",
            LastName = "LastName1",
            Email = "email1@email.com"
        };
        dbcontext.Add(user1);
        dbcontext.SaveChanges();
        dbcontext.ChangeTracker.Clear();
        var user2 = new User()
        {
            Name = "Name2",
            FirstName = "FirstName1",
            LastName = "LastName1",
            Email = "email1@email.com"
        };
        dbcontext.Add(user2);

        Assert.ThrowsException<DbUpdateException>(() => dbcontext.SaveChanges());

        DatabaseTestHelper.EnsureDatabaseDeleted(dbcontext);
    }

    [TestMethod]
    public void UserTableShouldHaveAlternateKeyOnName()
    {
        using var dbcontext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbcontext);

        var user1 = new User()
        {
            Name = "Name",
            FirstName = "FirstName1",
            LastName = "LastName1",
            Email = "email1@email.com"
        };
        dbcontext.Add(user1);
        dbcontext.SaveChanges();
        dbcontext.ChangeTracker.Clear();
        var user2 = new User()
        {
            Name = "Name",
            FirstName = "FirstName2",
            LastName = "LastName2",
            Email = "email2@email.com"
        };
        dbcontext.Add(user2);

        Assert.ThrowsException<DbUpdateException>(() => dbcontext.SaveChanges());

        DatabaseTestHelper.EnsureDatabaseDeleted(dbcontext);
    }

    [TestMethod]
    public void UserTableCanInsertThreeRowsWithDifferentUsers()
    {
        using var dbcontext = DatabaseTestHelper.CreatePropretteDbContext();
        DatabaseTestHelper.EnsureDatabaseCreated(dbcontext);
        DatabaseTestHelper.SeedUserTable(dbcontext);

        var users = dbcontext.Set<User>().Select(u => u.Name + u.FirstName + u.LastName + u.Email).ToList();

        Assert.AreEqual(3, users.Count);
        Assert.IsTrue(users.Contains("Name1FirstName1LastName1Email1"));
        Assert.IsTrue(users.Contains("Name2FirstName2LastName1Email1"));
        Assert.IsTrue(users.Contains("Name3FirstName2LastName2Email1"));

        DatabaseTestHelper.EnsureDatabaseDeleted(dbcontext);
    }
    #endregion


}
