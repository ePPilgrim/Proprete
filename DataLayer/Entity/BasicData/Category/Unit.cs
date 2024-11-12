namespace Proprette.DataLayer.Entity.BasicData.Category;

public class Unit(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public Unit() : this(null!)
    { }
}
