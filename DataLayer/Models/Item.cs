using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proprete.Data.Tables
{
    public enum ItemType
    {
        Floor,
        Window,
        Dish
    }

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ItemName { get; set; }
        public ItemType Type { get; set; }
        public Item(string itemName, ItemType type) {
            ItemName = itemName; 
            Type = type;
        }

    }
}
