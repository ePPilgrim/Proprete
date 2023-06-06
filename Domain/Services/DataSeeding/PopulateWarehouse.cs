using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Data.Models;
using Proprette.Domain.Data.Entities;
using System.Collections.Immutable;
using Proprette.Domain.Context;

namespace Proprette.Domain.Services.DataSeeding;

public class PopulateWarehouse : IPopulateTable
{
    private readonly PropretteDbContext context;

    public PopulateTable(PropretteDbContext dbContext)
    {
        context = dbContext ?? throw new ArgumentNullException("context");
    }

    public async Task InsertItems(IList<WarehouseDto> items)
    {
        var mapping = items
            .GroupBy(el => new { el.ItemName, el.ItemType })
            .Select(el => new { el.Key, Value = el.First() })
            .ToDictionary(el => el.Key, el => el.Value);
        var itemNameKeys = mapping.Keys.Select(el => el.ItemName).ToHashSet();
        var itemTypeKeys = mapping.Keys.Select(el => el.ItemType).ToHashSet();

        var baseQuery = context.Set<Item>().TagWith("PopulateTable->InsertItems():")
            .AsNoTracking()
            .Where(x => itemNameKeys.Contains(x.ItemName) && itemTypeKeys.Contains(x.ItemType));

        var rowsAsync = await baseQuery.ToListAsync();

        var rows = rowsAsync
            .Where(el => mapping.Keys.Contains(new { el.ItemName, el.ItemType }))
            .ToDictionary(el => new { el.ItemName, el.ItemType }, el => el);

        var toadd = mapping.Where(el => !rows.ContainsKey(el.Key)).Select(el => new Item(el.Value.ItemName)
        {
            ItemType = el.Value.ItemType
        }).ToList();

        if (toadd.Any())
        {
            await context.Set<Item>().AddRangeAsync(toadd);
            await context.SaveChangesAsync();
        }
    }

    public void Insert(IEnumerable<WarehouseDto> warehouses)
    {
        int i = 0;
        var toadd = warehouses.Select(
                el => new Warehouse(
                    new Item(el.ItemName)
                    {
                        ItemType = el.ItemType,
                        ItemID = ++i
                    }
                    )
                {
                    DateTime = el.DateTime,
                    Count = el.Count,
                    ItemID = i
                }
                ).ToList();

        var rows = context.Set<Warehouse>().TagWith("PopulateTable->Insert()")
           .Include(el => el.Item)
           .ToList();

        rows.AddRange(toadd);

        context.SaveChanges();

    }

    public void PopulateWarehouse(IEnumerable<WarehouseDto> warehouses)
    {
        var itemNameKeys = warehouses.Select(el => el.ItemName).ToHashSet();
        var itemTypeKeys = warehouses.Select(el => el.ItemType).ToHashSet();
        var warDataTimeKeys = warehouses.Select(el => el.DateTime).ToHashSet();
        var combineKeys = warehouses.Select(el => new { el.ItemName, el.ItemType }).ToHashSet();

        var itemRows = context.Set<Item>().TagWith("PopulateTable=>PopulateWarehouse():")
            .Where(el => itemNameKeys.Contains(el.ItemName) && itemTypeKeys.Contains(el.ItemType))
            .ToList()
            .Where(el => combineKeys.Contains(new { el.ItemName, el.ItemType }))
            .ToDictionary(el => el.ItemID, el => el);

        var warRowsAsync = context.Set<Warehouse>()
            .Where(el => itemRows.Keys.Contains(el.ItemID) && warDataTimeKeys.Contains(el.DateTime)).ToListAsync();

        var mapAltKeys = itemRows.ToDictionary(el => new { el.Value.ItemName, el.Value.ItemType }, el => el.Value.ItemID);
        foreach (var val in warehouses)
        {
            val.ItemID = mapAltKeys[new { val.ItemName, val.ItemType }];
        }
        var warCombineKeys = warehouses.Select(el => new { el.ItemID, el.DateTime }).ToHashSet();

        var wwarRows = warRowsAsync.Result;

        var warRows = wwarRows.
            Where(el => warCombineKeys.Contains(new { el.ItemID, el.DateTime }))
            .ToDictionary(el => new { el.ItemID, el.DateTime }, el => el);

        foreach (var val in warehouses)
        {
            Warehouse dbvalue;
            if (warRows.TryGetValue(new { val.ItemID, val.DateTime }, out dbvalue))
            {
                dbvalue.Count += val.Count;
            }
            else
            {
                context.Set<Warehouse>().Add(new Warehouse(itemRows[val.ItemID])
                {
                    ItemID = val.ItemID,
                    DateTime = val.DateTime,
                    Count = val.Count
                });
            }
        }
        context.SaveChanges();
    }

    public async Task Delete()
    {
        context.RemoveRange(context.Set<Item>());
        await context.SaveChangesAsync();
    }

    public Task UpdateOrInsert()
    {
        throw new NotImplementedException();
    }
}
