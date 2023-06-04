using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Entities;
using Proprette.Domain.Models;

namespace Proprette.Domain.Services;

public class PopulateTable
{
    private readonly PropretteDbContext context;

    public PopulateTable(PropretteDbContext dbContext)
    {
        context = dbContext ?? throw new ArgumentNullException("context");
    }

    public async Task PopulateWarehouse(IEnumerable<WarehouseDto> warehouses)
    {
        var mapping = warehouses.ToDictionary(el => new {el.ItemName, el.ItemType, el.DateTime});

        if(mapping.Keys.Count != warehouses.Count())
        {
            throw new ArgumentException("Input parameter should have unique combination of secondary keys");
        }

        var rows = context.Set<Warehouse>().TagWith("PopulateWarehouse")
            .Where(x => mapping.Keys.Contains(new { x.Item.ItemName, x.Item.ItemType, x.DateTime }))
            .Include(x => x.Item)
            .ToDictionary(el=>new {el.Item.ItemName, el.Item.ItemType, el.DateTime});

        
        foreach(var (key, val) in mapping){
            Warehouse value;
            if(rows.TryGetValue(key, out value))
            {
                value.Count += val.Count;
            }
            else
            {
                rows.Add(key, new Warehouse(
                    new Item(val.ItemName)
                    {
                        ItemType = val.ItemType,
                    })
                  { 
                    DateTime = val.DateTime,
                    Count = val.Count
                });
            }
        }

        await context.SaveChangesAsync();
    }


}
