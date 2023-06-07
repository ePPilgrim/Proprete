using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Data.Models;
using Proprette.DataSeeding;
using Proprette.Domain.Services.DataSeeding;
using Microsoft.Extensions.Logging;
using Proprette.Domain.Context;

Console.WriteLine("Input file name:");
//var filePath = Console.ReadLine();
string filePath = "proba.csv";
if(filePath == null)
{
    throw new ArgumentNullException(nameof(filePath));
}
var csvFile = new CsvFile<WarehouseDto>(filePath);
var memoryDataLayer = new Records();
var obj = csvFile.Read().ToList();

// Configure logging
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()  // Add console logging provider
        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);  // Filter logs for database commands
});

var dbContexOptionsBuilder = new DbContextOptionsBuilder<PropretteDbContext>();
dbContexOptionsBuilder
    .UseLoggerFactory(loggerFactory)
    .UseSqlite("Data Source=C:\\Users\\demyd\\Practice\\Proprette\\API\\Proprette.db", b => b.MigrationsAssembly("Service"));

Console.WriteLine("Choose the next option to process:");
Console.WriteLine("\tr - read from file to DB");
Console.WriteLine("\tw - write from memory to file");

switch (Console.ReadLine())
{
    case "i":
        //var obj = csvFile.Read().ToList();
        //using (var dbContext = new PropretteDbContext(dbContexOptionsBuilder.Options))
        //{
        //    var serv = new PopulateTable(dbContext);
        //    var res = serv.InsertItems(obj);
        //    res.Wait();
        //}
        break;
    case "r":
        using (var dbContext = new PropretteDbContext(dbContexOptionsBuilder.Options))
        {
            var serv = new PopulateItem(dbContext, obj);
            var res = serv.UpdateOrInsert();
            res.Wait();
        }
        //using (var dbContext = new PropretteDbContext(dbContexOptionsBuilder.Options))
        //{
        //    var serv = new PopulateTable(dbContext);
        //    //var res = 
        //     serv.PopulateWarehouse(obj.Skip(3).Take(4));
        //    //res.Wait();
        //}
        //using (var dbContext = new PropretteDbContext(dbContexOptionsBuilder.Options))
        //{
        //    var serv = new PopulateTable(dbContext);
        //    //var res = 
        //    serv.PopulateWarehouse(obj);
        //    //res.Wait();
        //}

        //dbContext.Add(new Location { LocationName = "Prague0" });
        //dbContext.SaveChanges();

        break;
    case "d":

        using (var dbContext = new PropretteDbContext(dbContexOptionsBuilder.Options))
        {
            var serv = new PopulateItem(dbContext, obj);
            var res = serv.Delete();
            res.Wait();
        }
        break;
    case "w":
        csvFile.Write(memoryDataLayer.WarehouseRecords);
        break;
}

