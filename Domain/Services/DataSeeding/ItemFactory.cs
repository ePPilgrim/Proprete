using Proprette.Domain.Context;
using Proprette.Domain.Data.Entities;
using Proprette.Domain.Data.Internals;
using Proprette.Domain.Services.DataSeeding.Internal;

namespace Proprette.Domain.Services.DataSeeding
{
    public class ItemFactory : IEntityFactory<Item>
    {
        private readonly PropretteDbContext context;

        public ItemFactory(PropretteDbContext context)
        {
            this.context = context;
        }

        public IDBCollection<Item> CreateCollectionShallow (IEnumerable<Item> values)
        {
            return new SetCollection<Item>(values, CreateComparer());
        }

        public IDBCollection<Item> CreateCollectionDeep(IEnumerable<Item> values)
        {
            return new SetCollection<Item>(values.Select(el => new Item(el.ItemName, el.ItemType)), CreateComparer());
        }

        public ICompareKeys<Item> CreateComparer()
        {
            return new CompareKeysItem();
        }

        public IPopulateTableInternal<Item> CreatePopulateInternal()
        {
            return new PopulateItemInternal(context);
        }
    }
}
