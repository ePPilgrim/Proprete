using Microsoft.EntityFrameworkCore;
using Proprette.API.Models;
using Proprette.DataSeeding;
using Proprette.Domain.Service;

Console.WriteLine("Input file name:");
var filePath = Console.ReadLine();
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
        dbContexOptionsBuilder.UseSqlite("Data Source=C:\\Users\\demyd\\Practice\\Proprete\\Service\\Proprete.db", b => b.MigrationsAssembly("Service"));
        var dbContext = new PropretteDbContext(dbContexOptionsBuilder.Options);
        var obj = csvFile.Read().ToList();


        //dbContext.Add(new Location { LocationName = "Prague0" });
        dbContext.SaveChanges();
        
        break;
    case "w":
        csvFile.Write(memoryDataLayer.WarehouseRecords);
        break;
}

