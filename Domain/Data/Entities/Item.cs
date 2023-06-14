using Proprette.Domain.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proprette.Domain.Data.Entities;

public class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ItemID { get; set; }

    [MaxLength(50)]
    public string ItemName { get; set; }
    public ItemType ItemType { get; set; }

    public Item(string itemName, ItemType itemType)
    {
        ItemName = itemName;
        ItemType = itemType;
    }

    public Item(Item item)
    {
        ItemID = item.ItemID;
        ItemName = item.ItemName;
        ItemType = item.ItemType;   
    }

    private Item() : this(null!, ItemType.None) { }
    

}
