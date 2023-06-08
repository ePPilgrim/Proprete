using Proprette.Domain.Data.Common;
using Proprette.Domain.Data.Entities;

namespace Proprette.Domain.Data.Internals;

internal class ItemInternal : ICompareKeys<ItemInternal>
{
    public readonly Item Item;
    public string ItemName => Item.ItemName;
    public ItemType ItemType => Item.ItemType;
    public int ItemID { get { return Item.ItemID; } set { Item.ItemID = value; } }

    public ItemInternal(Item item)
    {
        Item = item;
    }

    public int CompareInternalKey(ItemInternal other)
    {
        return ItemID - other.ItemID;
    }

    public int CompareAltKeys(ItemInternal other)
    {
        return CompareHelper.Compare(ItemName, ItemType, other.ItemName, other.ItemType);   
    }

    public override int GetHashCode()
    {
        return HashCodeHelper.Get(ItemName, ItemType);
    }
}
