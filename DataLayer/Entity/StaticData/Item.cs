using Proprette.DataLayer.Entity.BasicData.Category;

namespace Proprette.DataLayer.Entity.StaticData;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public int ItemTypeId { get; set; }
    public ItemType ItemType { get; set; } = null!;
    public int UsageId { get; set; }
    public Usage Usage { get; set; } = null!;
    public int ColorId { get; set; }
    public Color Color { get; set; } = null!;
    public int CapacityId { get; set; }
    public Capacity Capacity { get; set; } = null!;
    public int SizeId { get; set; }
    public Size Size { get; set; } = null!;
    public int UnitId { get; set; }
    public Unit Unit { get; set; } = null!;
    public int SubItemId { get; set; }
    public SubItem SubItem { get; set; } = null!;
    public int CompositionId { get; set; }
    public Composition Composition { get; set; } = null!;
    public int FreeCode1Id { get; set; }
    public FreeCode1 FreeCode1 { get; set; } = null!;
    public int FreeCode2Id { get; set; }
    public FreeCode2 FreeCode2 { get; set; } = null!;
    public int FreeCode3Id { get; set; }
    public FreeCode3 FreeCode3 { get; set; } = null!;
}
