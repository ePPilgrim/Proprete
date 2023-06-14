using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Internals;
using Proprette.Domain.Services.DataSeeding.Internal;

namespace Proprette.Domain.Services.DataSeeding
{
    public class WarehouseFactory : IEntityFactory<Warehouse>
    {
        private readonly PropretteDbContext context;
        private readonly IEntityFactory<Item> itemFactory;

        public WarehouseFactory(PropretteDbContext context,
                                IEntityFactory<Item> itemFactory)
        {
            this.context = context;
            this.itemFactory = itemFactory;
        }

        public IDBCollection<Warehouse> CreateCollectionShallow(IEnumerable<Warehouse> values)
        {
            return new WarehouseCollection(values, CreateComparer(), itemFactory);
        }

        public IDBCollection<Warehouse> CreateCollectionDeep(IEnumerable<Warehouse> values)
        {
            return new WarehouseCollection(values.Select(el => new Warehouse(el)), CreateComparer(), itemFactory);
        }

        public ICompareKeys<Warehouse> CreateComparer()
        {
            return new CompareKeysWarehouse();
        }

        public IPopulateTableInternal<Warehouse> CreatePopulateInternal()
        {
            return new PopulateWarehouseInternal(context, itemFactory);
        }
    }
}
