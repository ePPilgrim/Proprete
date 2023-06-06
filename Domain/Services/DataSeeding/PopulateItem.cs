using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proprette.Domain.Services.DataSeeding;

public class PopulateItem : IPopulateTable
{
    private readonly PropretteDbContext context;


    public PopulateItem(PropretteDbContext dbContext)
    {
        context = dbContext ?? throw new ArgumentNullException("context");
    }
    public async Task Delete()
    {
        context.RemoveRange(context.Set<Item>());
        await context.SaveChangesAsync();
    }

    public Task UpdateOrInsert(IList<WarehouseDto> items)
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
}
