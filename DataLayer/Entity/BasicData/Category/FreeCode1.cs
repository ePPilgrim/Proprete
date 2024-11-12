namespace Proprette.DataLayer.Entity.BasicData.Category;

public class FreeCode1(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public FreeCode1() : this(null!)
    { }
}
