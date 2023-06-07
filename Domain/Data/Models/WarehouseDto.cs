using Proprette.Domain.Data.Common;

namespace Proprette.Domain.Data.Models;
public class WarehouseDto
{
    public string ItemName { get; set; }
    public ItemType ItemType { get; set; }
    public DateTime DateTime { get; set; }
    public int Count { get; set; }

    public WarehouseDto(string itemName, ItemType itemType, DateTime dateTime, int count)
    {
        ItemName = itemName;
        ItemType = itemType;
        DateTime = dateTime;
        Count = count;
    }

    public WarehouseDto() { }   
}

