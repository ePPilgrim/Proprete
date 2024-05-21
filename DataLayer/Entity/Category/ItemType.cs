namespace Proprette.DataLayer.Entity.Category;

public class ItemType(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    public ItemType() : this(null!)
    { }
}
