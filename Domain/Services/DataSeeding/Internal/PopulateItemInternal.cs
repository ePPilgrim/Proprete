using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Internals;

namespace Proprette.Domain.Services.DataSeeding.Internal;

internal class PopulateItemInternal : IPopulateTable<ItemInternal>
{
    private readonly PropretteDbContext context;

    public PopulateItemInternal(PropretteDbContext dbContext)
    {
        context = dbContext ?? throw new ArgumentNullException("dbContext");
    }

    public async Task UpdateOrInsert(IEnumerable<ItemInternal> data)
    {
        var itemNameKeys = data.Select(el => el.ItemName).ToHashSet();
        var itemTypeKeys = data.Select(el => el.ItemType).ToHashSet();

        var rowsAsync = context.Set<Item>().TagWith("PopulateItem->UpdateOrInsert():")
            .AsNoTracking()
            .Where(x => itemNameKeys.Contains(x.ItemName) && itemTypeKeys.Contains(x.ItemType))
            .Select(x => new { x.ItemName, x.ItemType })
            .ToListAsync();

        var rowHashs = data.Select(el => HashCodeHelper.Get(el.ItemName, el.ItemType))
            .ToHashSet()
            .Except(rowsAsync.Result.Select(el => HashCodeHelper.Get(el.ItemName, el.ItemType)))
            .ToHashSet();

        var toadd = data.Where(el => rowHashs.Contains(HashCodeHelper.Get(el.ItemName, el.ItemType)));

        await Insert(toadd);
    }

    public async Task Insert(IEnumerable<ItemInternal> records)
    {
        if (records.Any())
        {
            await context.Set<Item>().AddRangeAsync(records.Select(el => el.Item).ToList());
            await context.SaveChangesAsync();
        }  
    }

    public async Task Delete()
    {
        context.RemoveRange(context.Set<Item>());
        await context.SaveChangesAsync();
    }
}
