namespace Proprette.DataLayer.Entity.Category;

public class Usage(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    private Usage() : this(null!)
    { }
}
