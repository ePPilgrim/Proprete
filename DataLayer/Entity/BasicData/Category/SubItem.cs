namespace Proprette.DataLayer.Entity.BasicData.Category;

public class SubItem(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public SubItem() : this(null!)
    { }
}
