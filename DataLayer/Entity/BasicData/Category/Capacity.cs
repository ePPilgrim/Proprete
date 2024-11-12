namespace Proprette.DataLayer.Entity.BasicData.Category;

public class Capacity(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    public Capacity() : this(null!)
    { }
}
