using DataLayer.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ItemName { get; set; }

        [Required]
        public ItemType Type { get; set; }

    }
}
