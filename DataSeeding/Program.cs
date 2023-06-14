using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Data.Models;
using Proprette.DataSeeding;
using Proprette.Domain.Services.DataSeeding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Proprette.Infrastructure;
using Proprette.Domain;

Console.WriteLine("Input file name:");
//var filePath = Console.ReadLine();
string filePath = "proba.csv";
if(filePath == null)
{
    throw new ArgumentNullException(nameof(filePath));
}
var csvFile = new CsvFile<WarehouseDto>(filePath);
var memoryDataLayer = new Records();
var obj1 = csvFile.Read().Take(3).ToList();
var obj = csvFile.Read().ToList();

// Configure logging
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()  // Add console logging provider
        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);  // Filter logs for database commands
});

var serviceCollection = new ServiceCollection();   
serviceCollection.AddSingleton(loggerFactory);
serviceCollection.AddInfrastructure("Data Source=C:\\Users\\demyd\\Practice\\Proprette\\API\\Proprette.db");
serviceCollection.AddDomain();
var services = serviceCollection.BuildServiceProvider();

Console.WriteLine("Choose the next option to process:");
Console.WriteLine("\tr - read from file to DB");
Console.WriteLine("\tw - write from memory to file");

switch (Console.ReadLine())
{
    case "r":
        using(var scope = services.CreateScope())
        {
            var populator = services.GetService<IPopulateTable<WarehouseDto>>();
            var res = populator.UpdateOrInsert(obj);// obj.Take(3));
            res.Wait();
        }
        break;
    case "d":
        using (var scope = services.CreateScope())
        {
            //var services = scope.ServiceProvider;
            var populator = services.GetService<IPopulateTable<WarehouseDto>>();
            var res = populator.Delete();
            res.Wait();

        }
        break;
    case "w":
        csvFile.Write(memoryDataLayer.WarehouseRecords);
        break;
}

