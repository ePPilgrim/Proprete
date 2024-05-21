namespace Proprette.DataLayer.Entity.Category;

public class SubItem(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    public SubItem() : this(null!)
    { }
}
