using System.ComponentModel.DataAnnotations.Schema;


namespace Proprette.Domain.Models
{
    public class SubWarehouse
    {
        public int LocationID { get; set; }
        public int ItemID { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }

        [ForeignKey("LocationID")]
        public Location Location { get; set;}

        [ForeignKey("ItemID")]
        public Item Item{ get; set; }

        public SubWarehouse(Location location, Item item)
        {
            Location = location;
            Item = item;
        }
    }
}
