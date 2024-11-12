namespace Proprette.DataLayer.Entity.BasicData.Category;

public class Size(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public Size() : this(null!)
    { }
}
