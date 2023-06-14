using Proprette.Domain.Data.Common;
using Proprette.Domain.Data.Entities;

namespace Proprette.Domain.Data.Internals
{
    public static class DBCollectionExtensions
    {
        public static HashSet<string> GetItemNameKeys(this IDBCollection<Item> items)
        {
            return items.Values.Select(el => el.ItemName).ToHashSet();
        }

        public static HashSet<ItemType> GetItemTypeKeys(this IDBCollection<Item> items)
        {
            return items.Values.Select(el => el.ItemType).ToHashSet();
        }

        public static HashSet<int> GetItemIdKeys(this IDBCollection<Warehouse> warehouses)
        {
            return warehouses.Values.Select(el => el.ItemID).ToHashSet();
        }

        public static HashSet<DateTime> GetDataTimeKeys(this IDBCollection<Warehouse> warehouses)
        {
            return warehouses.Values.Select(el => el.DateTime).ToHashSet();
        }

        public static IDBCollection<Item>? GetItems(this IDBCollection<Warehouse> warehouses) {
            return warehouses.GetProperty("Item") as IDBCollection<Item>;
        }
    }
}
