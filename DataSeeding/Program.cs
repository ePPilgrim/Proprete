using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Models;
using Proprette.DataSeeding;
using Proprette.Domain.Services;

Console.WriteLine("Input file name:");
//var filePath = Console.ReadLine();
string filePath = "proba.csv";
if(filePath == null)
{
    throw new ArgumentNullException(nameof(filePath));
}
var csvFile = new CsvFile<WarehouseDto>(filePath);
var memoryDataLayer = new Records();

Console.WriteLine("Choose the next option to process:");
Console.WriteLine("\tr - read from file to DB");
Console.WriteLine("\tw - write from memory to file");

switch (Console.ReadLine())
{
    case "r":
        var dbContexOptionsBuilder = new DbContextOptionsBuilder<PropretteDbContext>();
        dbContexOptionsBuilder.UseSqlite("Data Source=C:\\Users\\demyd\\Practice\\Proprette\\API\\Proprette.db", b => b.MigrationsAssembly("Service"));
        var obj = csvFile.Read().ToList();

        using (var dbContext = new PropretteDbContext(dbContexOptionsBuilder.Options))
        {
            var serv = new PopulateTable(dbContext);
            var res = serv.PopulateWarehouse(obj.Take(3));
            res.Wait();
        }

            //dbContext.Add(new Location { LocationName = "Prague0" });
            //dbContext.SaveChanges();
        
        break;
    case "w":
        csvFile.Write(memoryDataLayer.WarehouseRecords);
        break;
}

