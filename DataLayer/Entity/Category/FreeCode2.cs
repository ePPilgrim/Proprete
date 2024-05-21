namespace Proprette.DataLayer.Entity.Category;

public class FreeCode2(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    private FreeCode2() : this(null!)
    { }
}
