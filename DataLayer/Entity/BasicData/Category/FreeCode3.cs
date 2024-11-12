namespace Proprette.DataLayer.Entity.BasicData.Category;

public class FreeCode3(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public FreeCode3() : this(null!)
    { }
}
