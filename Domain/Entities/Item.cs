using Proprette.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proprette.Domain.Entities;

public class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ItemID { get; set; }

    [MaxLength(50)]
    public string ItemName { get; set; }
    public ItemType ItemType { get; set; }

    public Item(string itemName)
    {
        ItemName = itemName;
    }

}
