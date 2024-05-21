namespace Proprette.DataLayer.Entity.Category;

public class Using(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    private Using() : this(null!)
    { }
}
