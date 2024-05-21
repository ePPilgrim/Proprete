namespace Proprette.DataLayer.Entity.Category;

public class FreeCode1(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    private FreeCode1() : this(null!)
    { }
}
