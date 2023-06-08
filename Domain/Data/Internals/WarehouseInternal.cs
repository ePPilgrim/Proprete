using Proprette.Domain.Data.Common;
using Proprette.Domain.Data.Entities;

namespace Proprette.Domain.Data.Internals
{
    internal class WarehouseInternal : ICompareKeys<WarehouseInternal>
    {
        public readonly Warehouse Warehouse;

        public int ItemID { get { return Warehouse.ItemID; } set { Warehouse.ItemID = value; } }
        public DateTime DateTime => Warehouse.DateTime;
        public int Count => Warehouse.Count;
        public string ItemName => Warehouse.Item.ItemName;
        public ItemType ItemType => Warehouse.Item.ItemType;

        public WarehouseInternal(Warehouse warehouse)
        {
            Warehouse = warehouse;
        }

        public int CompareAltKeys(WarehouseInternal other)
        {
            return CompareHelper.Compare(ItemID, DateTime, other.ItemID, other.DateTime);
        }

        public int CompareInternalKey(WarehouseInternal other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.Get(ItemID, DateTime);
        }
    }
}
