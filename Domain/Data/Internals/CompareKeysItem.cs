using Proprette.Domain.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Proprette.Domain.Data.Internals
{
    public class CompareKeysItem : ICompareKeys<Item> 
    {
        public int Compare(Item? item1, Item? item2)
        {
            _= item1 ?? throw new ArgumentNullException(nameof(item1));
            _= item2 ?? throw new ArgumentNullException(nameof(item2));
            return CompareHelper.Compare(item1.ItemName, item1.ItemType, item2.ItemName, item2.ItemType);
        }

        public bool Equals(Item? item1, Item? item2)
        {
            _ = item1 ?? throw new ArgumentNullException(nameof(item1));
            _ = item2 ?? throw new ArgumentNullException(nameof(item2));
            return CompareHelper.Compare(item1.ItemName, item1.ItemType, item2.ItemName, item2.ItemType) == 0;
        }

        public int GetHashCode([DisallowNull] Item item)
        {
            return HashCodeHelper.Get(item.ItemName, item.ItemType);
        }
    }
}
