using System.ComponentModel.DataAnnotations.Schema;

namespace Proprette.Domain.Data.Entities;

public class Warehouse
{
    public int ItemID { get; set; }
    public DateTime DateTime { get; set; }
    public int Count { get; set; }

    [ForeignKey("ItemID")]
    public Item Item { get; set; }

    public Warehouse(Warehouse warehouse)
    {
        ItemID = warehouse.ItemID;
        DateTime = warehouse.DateTime;
        Count = warehouse.Count;
        Item = new Item(warehouse.Item);
    }

    private Warehouse()  {
        Item = null!;
    }
}
