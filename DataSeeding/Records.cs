using Proprette.Domain.Data.Common;
using Proprette.Domain.Data .Models;

namespace Proprette.DataSeeding;
internal class Records
{
    private readonly List<WarehouseDto> _warehouse = new List<WarehouseDto>
        {
            new WarehouseDto("Wanish", ItemType.Floor, new DateTime(2023, 01, 01), 10),
            new WarehouseDto("Wanish", ItemType.Floor, new DateTime(2023, 02, 01), 7),
            new WarehouseDto("Wanish", ItemType.Window, new DateTime(2023, 01, 01), 9),
            new WarehouseDto("Wanish", ItemType.Window, new DateTime(2023, 02, 01), 6),
            new WarehouseDto("Wanish", ItemType.Ceil, new DateTime(2023, 01, 01), 8),
            new WarehouseDto("Wanish", ItemType.Ceil, new DateTime(2023, 02, 01), 5),

            new WarehouseDto("Domestos", ItemType.Floor, new DateTime(2023, 01, 01), 20),
            new WarehouseDto("Domestos", ItemType.Window, new DateTime(2023, 01, 01), 19),
            new WarehouseDto("Domestos", ItemType.Ceil, new DateTime(2023, 01, 01), 18),
            new WarehouseDto("Domestos", ItemType.Floor, new DateTime(2023, 02, 01), 17),
            new WarehouseDto("Domestos", ItemType.Window, new DateTime(2023, 02, 01), 16),
            new WarehouseDto("Domestos", ItemType.Ceil, new DateTime(2023, 02, 01), 15),
        };




    //private readonly List<SubWarehouseDto> _subWarehouse;

    public List<WarehouseDto> WarehouseRecords { get { return _warehouse; } }
    //public List<SubWarehouseDto> SubWarehouseRecords { get { return _subWarehouse; } }


}

