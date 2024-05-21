namespace Proprette.DataLayer.Entity.Category;

public class FreeCode3(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    private FreeCode3() : this(null!)
    { }
}
