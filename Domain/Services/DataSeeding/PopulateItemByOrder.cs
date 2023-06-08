//using Microsoft.EntityFrameworkCore;
//using Proprette.Domain.Context;
//using Proprette.Domain.Data.Entities;
//using Proprette.Domain.Data.Internals;
//using Proprette.Domain.Data.Models;

//namespace Proprette.Domain.Services.DataSeeding;

//public class PopulateItemByOrder : IPopulateTable<>
//{
//    private readonly PropretteDbContext context;
//    private readonly List<ItemInternal> itemRecords;


//    public PopulateItemByOrder(PropretteDbContext dbContext, IList<WarehouseDto> records)
//    {
//        context = dbContext ?? throw new ArgumentNullException("context");
//        itemRecords = records.Select(el => GetInternalItem(el))
//            .ToHashSet(new ComparerAltKeys<ItemInternal>())
//            .ToList();
//        itemRecords.Sort(new ComparerAltKeys<ItemInternal>());
//    }

//    public async Task Delete()
//    {
//        context.RemoveRange(context.Set<Item>());
//        await context.SaveChangesAsync();
//    }

//    public async Task UpdateOrInsert()
//    {
//        var itemNameKeys = itemRecords.Select(el => el.ItemName).ToHashSet();
//        var itemTypeKeys = itemRecords.Select(el => el.ItemType).ToHashSet();

//        var rowsAsyncs = await context.Set<Item>().TagWith("PopulateItemByOrder->UpdateOrInsert():")
//            .AsNoTracking()
//            .Where(x => itemNameKeys.Contains(x.ItemName) && itemTypeKeys.Contains(x.ItemType))
//            .Select(el => new { el.ItemName, el.ItemType })
//            .ToListAsync();

//        var itemKeys = itemRecords.Select(el => el.GetHashCode()).ToHashSet();
//        var row = rowsAsyncs.Where(el => itemKeys.Contains(HashCodeHelper.Get(el.ItemName, el.ItemType))).Max();

//        var toadd = itemRecords.Select(el => new Item(el.ItemName, el.ItemType));
//        if(row != null)
//        {
//            toadd = itemRecords
//                .Where(el => el.CompareAltKeys(new InternalItem(row.ItemName, row.ItemType)) > 0)
//                .Select(el => new Item(el.ItemName, el.ItemType));
//        }

//        if (toadd.Any())
//        {
//            await context.Set<Item>().AddRangeAsync(toadd);
//            await context.SaveChangesAsync();
//        }
//    }

//    private ItemInternal GetInternalItem(WarehouseDto obj)
//    {
//        return new InternalItem(obj.ItemName, obj.ItemType)
//        {
//            ItemID = 0,
//            Item = new Item(obj.ItemName, obj.ItemType)
//            {
//                ItemName = obj.ItemName,
//                ItemType = obj.ItemType,
//                ItemID = 0
//            }
//        };
//    }
//}

