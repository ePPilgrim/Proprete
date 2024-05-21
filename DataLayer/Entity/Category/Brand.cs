namespace Proprette.DataLayer.Entity.Category;

public class Brand(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    private Brand() : this(null!)
    {}
}
