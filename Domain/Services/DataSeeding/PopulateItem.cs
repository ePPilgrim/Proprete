using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Internals;
using Proprette.Domain.Data.Models;
using System.Collections.Immutable;

namespace Proprette.Domain.Services.DataSeeding;

public class PopulateItem : IPopulateTable
{
    private readonly PropretteDbContext context;
    private readonly HashSet<InternalItem> itemSet;


    public PopulateItem(PropretteDbContext dbContext, IList<WarehouseDto> records)
    {
        context = dbContext ?? throw new ArgumentNullException("context");
        itemSet = records.Select(el => GetInternalItem(el)).ToHashSet(new ComparerAltKeys<InternalItem>());
    }

    public async Task Delete()
    {
        context.RemoveRange(context.Set<Item>());
        await context.SaveChangesAsync();
    }

    public async Task UpdateOrInsert()
    {
        var itemNameKeys = itemSet.Select(el => el.ItemName).ToHashSet();
        var itemTypeKeys = itemSet.Select(el => el.ItemType).ToHashSet();

        var rows = await context.Set<Item>().TagWith("PopulateItem->UpdateOrInsert():")
            .AsNoTracking()
            .Where(x => itemNameKeys.Contains(x.ItemName) && itemTypeKeys.Contains(x.ItemType))
            .Select(x => new { x.ItemName, x.ItemType })
            .ToListAsync();

        var hashVals = rows.Select(el=>HashCodeHelper.Get(el.ItemName, el.ItemType));
        var toadd = itemSet
            .Where(el => !hashVals.Contains(HashCodeHelper.Get(el.ItemName, el.ItemType)))
            .Select(el => new Item(el.ItemName, el.ItemType))
            .ToList();

        if (toadd.Any())
        {
            await context.Set<Item>().AddRangeAsync(toadd);
            await context.SaveChangesAsync();
        }
    }

    private InternalItem GetInternalItem(WarehouseDto obj)
    {
        return new InternalItem(obj.ItemName, obj.ItemType)
        {
            ItemID = 0,
            Item = new Item(obj.ItemName, obj.ItemType)
            {
                ItemName = obj.ItemName,
                ItemType = obj.ItemType,
                ItemID = 0
            }
        };
    }
}
