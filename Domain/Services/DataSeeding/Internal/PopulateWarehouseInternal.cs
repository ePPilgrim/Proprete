using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Internals;

namespace Proprette.Domain.Services.DataSeeding.Internal;

internal class PopulateWarehouseInternal : IPopulateTable<WarehouseInternal>
{
    private readonly PropretteDbContext context;
    private readonly IPopulateTable<ItemInternal> itemTable;

    public PopulateWarehouseInternal(PropretteDbContext dbContext, IPopulateTable<ItemInternal> items)
    {
        context = dbContext ?? throw new ArgumentNullException("dbContext");
        itemTable = items;
    }

    public async Task Delete()
    {
        await itemTable.Delete();
    }

    public async Task Insert(IEnumerable<WarehouseInternal> records)
    {
        if (records.Any())
        {
            await context.Set<Warehouse>().AddRangeAsync(records.Select(el => el.Warehouse).ToList());
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateOrInsert(IEnumerable<WarehouseInternal> records)
    {
        await itemTable.UpdateOrInsert(records.Select(el => new ItemInternal(el.Warehouse.Item)));
        var itemNameKeys = records.Select(el=>el.ItemName).ToHashSet();
        var itemTypeKeys = records.Select(el=>el.ItemType).ToHashSet();
        var itemCombKeys = records.Select(el=>HashCodeHelper.Get(el.ItemName, el.ItemType)).ToHashSet();

        var itemMapAsync = await context.Set<Item>()
            .TagWith("PopulateWarehouseInternal->UpdateOrInsert()->1:")
            .AsNoTracking()
            .Where(el => itemNameKeys.Contains(el.ItemName) && itemTypeKeys.Contains(el.ItemType))
            .ToListAsync();

        var itemMap = itemMapAsync
            .Where(el => itemCombKeys.Contains(HashCodeHelper.Get(el.ItemName, el.ItemType)))
            .ToDictionary(el => HashCodeHelper.Get(el.ItemName, el.ItemType), el => el.ItemID);

        var dateTimeKeys = records.Select(el=>el.DateTime).ToHashSet();

        var rowsAsync = context.Set<Warehouse>()
            .TagWith("PopulateWarehouseInternal->UpdateOrInsert()->2:")
            .Where(el => dateTimeKeys.Contains(el.DateTime) && itemMap.Values.Contains(el.ItemID))
            .ToListAsync();

        foreach (var record in records) 
        {
            record.ItemID = itemMap[HashCodeHelper.Get(record.ItemName, record.ItemType)];
        }

        var map = records.ToDictionary(el => HashCodeHelper.Get(el.ItemID, el.DateTime), el => el);
        var rows = rowsAsync.Result
            .Where(el => map.ContainsKey(HashCodeHelper.Get(el.ItemID, el.DateTime)))
            .ToDictionary(el => HashCodeHelper.Get(el.ItemID, el.DateTime), el => el);
        var toadd = new List<WarehouseInternal>();
        foreach (var (key, val) in map) 
        {
            if (rows.TryGetValue(key, out Warehouse value))
            {
                value.Count += val.Count;
            }
            else
            {
                toadd.Add(val);
            }
        }

        await Insert(toadd);
        await context.SaveChangesAsync();
    }
}
