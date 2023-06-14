using Proprette.Domain.Data.Entities;
using Proprette.Domain.Services.DataSeeding;

namespace Proprette.Domain.Data.Internals
{
    public class WarehouseCollection : SetCollection<Warehouse>
    {
        private readonly HashSet<Warehouse> warehouses;
        private readonly IDBCollection<Item> items;

        public override IEnumerable<Warehouse> Values => warehouses;

        public WarehouseCollection(IEnumerable<Warehouse> warehouses, 
                                    ICompareKeys<Warehouse> warehouseCmp, 
                                    IEntityFactory<Item> itemFactory)
        {
            items = itemFactory.CreateCollectionShallow(warehouses.Select(el => el.Item));
            this.warehouses = createInitSetStructure(warehouses, warehouseCmp);  
        }

        public override Warehouse? Find(object obj)
        {
            if (warehouses.TryGetValue((Warehouse)obj, out Warehouse? res))
                return res;
            return default;
        }

        public override bool Remove(object obj)
        {
            return warehouses.Remove((Warehouse)obj);
        }

        public override IDBCollection<object>? GetProperty(string propertyName)
        {
            if(propertyName == "Item")
            {
                return items as IDBCollection<object>;
            }
            return base.GetProperty(propertyName);
        }

        private HashSet<Warehouse> createInitSetStructure(IEnumerable<Warehouse> warehouses, ICompareKeys<Warehouse> warehouseCmp)
        {
            foreach (var val in warehouses)
            {
                val.Item = items.Find(val.Item) ?? throw new NullReferenceException($"Subset of items must contain {val.Item}");
            }
            return warehouses.ToHashSet(warehouseCmp);
        }
    }
}
