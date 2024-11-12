namespace Proprette.DataLayer.Entity.BasicData.Category;

public class FreeCode2(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public FreeCode2() : this(null!)
    { }
}
