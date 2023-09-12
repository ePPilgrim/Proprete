using Microsoft.EntityFrameworkCore;
using Proprette.Domain.Data.Entities.Category;
using Proprette.NewDomain.Context;

namespace Proprette.NewDomain.Services;

public class CategoryRequests<TCategory> where TCategory : class, ICategory, new()
{
    private readonly PropretteDbContext context;

    public CategoryRequests(PropretteDbContext context)
    {
        this.context = context;
    }

    public async Task<IDictionary<string, int>> FetchAllAsync()
    {
        var res = await context.Set<TCategory>()
            .TagWith($"Fetch all rows from {nameof(TCategory)} entity.")
            .AsNoTracking()
            .Select(x => KeyValuePair.Create(x.Name, x.Id))
            .ToListAsync();
        return res.ToDictionary(x => x.Key, x => x.Value);
    }

    public async Task<IDictionary<string, int>> Add(HashSet<string> categoriesNames)
    {
        var res = (await FetchAllAsync()).Where(x => categoriesNames.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
        if (res.Count == categoriesNames.Count)
        {
            return res;
        }
        var namesToInsert = categoriesNames.Except(res.Keys).ToList();
        context.Set<TCategory>().AddRange(namesToInsert.Select(x => new TCategory() { Name = x }));

        await context.SaveChangesAsync();

        return (await FetchAllAsync()).Where(x => categoriesNames.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
    }


}
