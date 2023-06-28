using Proprette.Domain.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Proprette.Domain.Data.Models;
public class WarehouseDto
{
    public string ItemName { get; set; }
    public ItemType ItemType { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateTime { get; set; }
    public int Count { get; set; }

    public WarehouseDto(string itemName, ItemType itemType, DateTime dateTime, int count)
    {
        ItemName = itemName;
        ItemType = itemType;
        DateTime = dateTime;
        Count = count;
    }

    public WarehouseDto() : this(null!, ItemType.None, DateTime.MinValue, 0) { }

}

