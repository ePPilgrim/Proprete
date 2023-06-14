using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Internals;

namespace Proprette.Domain.Services.DataSeeding.Internal;

internal class PopulateItemInternal : IPopulateTableInternal<Item>
{
    private readonly PropretteDbContext context;

    public PopulateItemInternal(PropretteDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task UpdateOrInsert(IDBCollection<Item> data)
    {
        var itemNameKeys = data.GetItemNameKeys();
        var itemTypeKeys = data.GetItemTypeKeys();

        var rowsAsync = await context.Set<Item>().TagWith("PopulateItemInternal->UpdateOrInsert():")
            .AsNoTracking()
            .Where(x => itemNameKeys.Contains(x.ItemName) && itemTypeKeys.Contains(x.ItemType))
            .Select(x => new { x.ItemName, x.ItemType })
            .Select(el => HashCodeHelper.Get(el.ItemName, el.ItemType))
            .ToListAsync(); 

       var rowHashs = rowsAsync.ToHashSet();

        foreach (var val in data.Values)
        {
            if(rowsAsync.Contains(HashCodeHelper.Get(val.ItemName, val.ItemType)))
            {
                data.Remove(val);
            }
        }

        var res = Insert(data);
    }

    public async Task Insert(IDBCollection<Item> records)
    {
        if (records.Values.Any())
        {
            await context.Set<Item>().AddRangeAsync(records.Values);
            await context.SaveChangesAsync();
        }  
    }

    public async Task Delete()
    {
        context.RemoveRange(context.Set<Item>());
        await context.SaveChangesAsync();
    }
}
