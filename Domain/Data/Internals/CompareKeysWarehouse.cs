using Proprette.Domain.Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Proprette.Domain.Data.Internals
{
    internal class CompareKeysWarehouse : ICompareKeys<Warehouse>
    {
        public int Compare(Warehouse? obj1, Warehouse? obj2)
        {
            _ = obj1 ?? throw new ArgumentNullException(nameof(obj1));
            _ = obj2 ?? throw new ArgumentNullException(nameof(obj2));
            return CompareHelper.Compare(obj1.Item.ItemName, obj1.Item.ItemType, obj1.DateTime, obj2.Item.ItemName, obj2.Item.ItemType, obj2.DateTime);
        }

        public bool Equals(Warehouse? obj1, Warehouse? obj2)
        {
            return Compare(obj1, obj2) == 0;
        }

        public int GetHashCode([DisallowNull] Warehouse obj)
        {
            return HashCodeHelper.Get(obj.Item.ItemName, obj.Item.ItemType, obj.DateTime);
        }
    }
}
