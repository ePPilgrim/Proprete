namespace Proprette.DataLayer.Entity.BasicData.Category;

public class Color(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    public Color() : this(null!)
    { }
}