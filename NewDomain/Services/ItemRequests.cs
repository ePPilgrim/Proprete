using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Data.Entities.Category;
using Proprette.NewDomain.Context;
using Proprette.NewDomain.Data.Entities;
using Proprette.NewDomain.Data.Models;

namespace Proprette.NewDomain.Services;

public class ItemRequests
{
    private readonly PropretteDbContext context;

    public ItemRequests(PropretteDbContext context)
    {
        this.context = context;
    }

    public async Task<IDictionary<int, string[]>> FindAsync(string[] prefixes)
    {
        var items = await context.Set<Item>()
            .TagWith("Fetch all items.")
            .AsNoTracking()
            .Include(item => item.Brand)
            .Include(item => item.Color)
            .Select(item => KeyValuePair.Create(item.Id, new[]
            {
                item.Brand.Name,
                item.Color.Name
            }))
            .ToListAsync();

        return items.Where(item => includePrefixes(prefixes, item.Value)).ToDictionary(item => item.Key, item => item.Value);
    }

    public async Task<IEnumerable<int>> InsertAsync(ItemDto[] items)
    {
        var brands = items.Select(item => item.Brand).ToHashSet();
        var brandRequest = new CategoryRequests<Brand>(context);
        var brandMap = await brandRequest.Add(brands);

        var colors = items.Select(item => item.Color).ToHashSet();
        var colorRequest = new CategoryRequests<Color>(context);
        var colorMap = await colorRequest.Add(colors);

        var dbItems = await context.Set<Item>()
            .TagWith("Fetch all items")
            .ToListAsync();

        var uniqueRows = dbItems.Select(x => mergeToString(x.BrandId, x.ColorId)).ToHashSet();
        foreach (var itemDto in items)
        {
            var id = mergeToString(brandMap[itemDto.Brand], colorMap[itemDto.Color]);
            //if (uniqueRows.Contains(id))
            //{
            //    continue;
            //}
            await context.Set<Item>().AddAsync(new Item
            {
                BrandId = brandMap[itemDto.Brand],
                ColorId = colorMap[itemDto.Color],
            });
        }

        try
        {
            // await context.SaveChangesAsync();

            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }


        var allItems = (await context.Set<Item>()
            .TagWith("Fetch all items")
            .AsNoTracking()
            .Where(x =>
                brandMap.Values.Contains(x.BrandId)
                && colorMap.Values.Contains(x.ColorId)
                )
            .Select(x => KeyValuePair.Create(mergeToString(x.BrandId, x.ColorId), x.Id))
            .ToListAsync()).ToDictionary(x => x.Key, x => x.Value);
        var ids = items.Select(x => mergeToString(brandMap[x.Brand], colorMap[x.Color])).ToList();
        return ids.Select(id => allItems[id]);
    }

    static string mergeToString(int brand, int color)
    {
        return $"{brand}{color}";
    }

    static bool includePrefixes(string[] prefixes, string[] categoriesNames)
    {
        var res = true;
        for (int i = 0; i < prefixes.Length; ++i)
        {
            if (!categoriesNames[i].StartsWith(prefixes[i]))
            {
                return false;
            }
        }

        return res;
    }

}
