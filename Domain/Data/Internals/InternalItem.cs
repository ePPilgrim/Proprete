using Proprette.Domain.Data.Common;
using Proprette.Domain.Data.Entities;

namespace Proprette.Domain.Data.Internals;

internal class InternalItem : ICompareKeys<InternalItem>
{
    public int ItemID { get; set; }
    public string ItemName { get; set; }
    public ItemType ItemType { get; set; }
    public Item? Item { get;set; }

    public InternalItem(string itemName, ItemType itemType)
    {
        ItemName = itemName;
        ItemType = itemType;
    }

    public int CompareInternalKey(InternalItem other)
    {
        return ItemID - other.ItemID;
    }

    public int CompareAltKeys(InternalItem other)
    {
        return CompareHelper.Compare(ItemName, ItemType, other.ItemName, other.ItemType);   
    }

    public override int GetHashCode()
    {
        return HashCodeHelper.Get(ItemName, ItemType);
    }
}
