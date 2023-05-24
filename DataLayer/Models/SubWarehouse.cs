using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models
{
    public class SubWarehouse
    {
        public int LocationID { get; set; }
        public int ItemID { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }

        [ForeignKey("LocationID")]
        [Required]
        public Location Location { get; set;}

        [ForeignKey("ItemID")]
        [Required]
        public Item Item{ get; set; }
    }
}
