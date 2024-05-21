namespace Proprette.DataLayer.Entity.Category;

public class Capacity(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    private Capacity() : this(null!)
    { }
}
