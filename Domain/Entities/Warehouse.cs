using System.ComponentModel.DataAnnotations.Schema;

namespace Proprette.Domain.Entities;

public class Warehouse
{
    public int ItemID { get; set; }
    public DateTime DateTime { get; set; }
    public int Count { get; set; }

    [ForeignKey("ItemID")]
    public Item Item { get; set; }

    public Warehouse(Item item)
    {
        Item = item;
    }

    private Warehouse() : this(null!) { }
}
