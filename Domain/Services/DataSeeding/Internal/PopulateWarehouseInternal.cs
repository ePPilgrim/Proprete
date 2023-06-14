using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Internals;

namespace Proprette.Domain.Services.DataSeeding.Internal;

internal class PopulateWarehouseInternal : IPopulateTableInternal<Warehouse>
{
    private readonly PropretteDbContext context;
    private readonly IPopulateTableInternal<Item> itemTable;
    private readonly IEntityFactory<Item> entityFactory;    

    public PopulateWarehouseInternal(PropretteDbContext context,
        IEntityFactory<Item> entityFactory)
    {
        this.context = context;
        this.itemTable = entityFactory.CreatePopulateInternal();
        this.entityFactory = entityFactory;
    }

    public async Task Delete()
    {
        await itemTable.Delete();
    }

    public async Task Insert(IDBCollection<Warehouse> records)
    {
        if (records.Values.Any())
        {
            await context.Set<Warehouse>().AddRangeAsync(records.Values);
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateOrInsert(IDBCollection<Warehouse> records)
    {
        var itemRecords = records.GetItems() ?? throw new NullReferenceException($"No Item properties could be fetch from {nameof(records)}");
        await itemTable.UpdateOrInsert(entityFactory.CreateCollectionDeep(itemRecords.Values));
        var itemNameKeys = itemRecords.GetItemNameKeys();
        var itemTypeKeys = itemRecords.GetItemTypeKeys();

        var itemMapAsync= await context.Set<Item>()
            .TagWith("PopulateWarehouseInternal->UpdateOrInsert()->1:")
            .AsNoTracking()
            .Where(el => itemNameKeys.Contains(el.ItemName) && itemTypeKeys.Contains(el.ItemType))
            .Select(el => new {el.ItemName, el.ItemType, el.ItemID})
            .Select(el => new {Key = HashCodeHelper.Get(el.ItemName, el.ItemType), Value = el.ItemID})
            .ToListAsync();

        var itemMap = itemMapAsync.ToDictionary(el => el.Key, el=>el.Value);

        foreach (var val in records.Values)
        {
            if(itemMap.TryGetValue(HashCodeHelper.Get(val.Item.ItemName, val.Item.ItemType), out var itemId))
            {
                val.ItemID = itemId;
            }
        }

        var itemIDKeys = records.GetItemIdKeys();
        var dateTimeKeys = records.GetDataTimeKeys();
  
        var rowsAsync = await context.Set<Warehouse>()
            .TagWith("PopulateWarehouseInternal->UpdateOrInsert()->2:")
            .Where(el => itemIDKeys.Contains(el.ItemID) && dateTimeKeys.Contains(el.DateTime))
            .ToListAsync();

        var rows = rowsAsync.ToDictionary(el => HashCodeHelper.Get(el.ItemID, el.DateTime), el => el);

        foreach (var val in records.Values)
        {
            if( rows.TryGetValue(HashCodeHelper.Get(val.ItemID, val.DateTime), out Warehouse? value)){
                value.Count += val.Count;
                records.Remove(val);
            }
            else
            {
                val.Item = null!;
            }
        }

        await Insert(records);
        await context.SaveChangesAsync();
    }
}
