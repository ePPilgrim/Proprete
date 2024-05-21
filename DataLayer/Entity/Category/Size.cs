namespace Proprette.DataLayer.Entity.Category;

public class Size(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    private Size() : this(null!)
    { }
}
