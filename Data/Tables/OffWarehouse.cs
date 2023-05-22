using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proprete.Data.Tables
{
    public class OffWarehouse
    {
        public int LocationID { get; set; }
        public int ItemID { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("LocationID")]
        [Required]
        public Location Location { get; set;}

        [ForeignKey("ItemID")]
        [Required]
        public Item Item{ get; set; }
    }
}
