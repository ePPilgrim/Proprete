using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Warehouse
    {
        public int ItemID { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }

        [ForeignKey("ItemID")]
        [Required]
        public Item Item { get; set; }
    }
}
