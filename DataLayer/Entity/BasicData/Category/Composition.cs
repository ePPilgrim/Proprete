namespace Proprette.DataLayer.Entity.BasicData.Category;

public class Composition(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public Composition() : this(null!)
    { }
}
