namespace Proprette.Domain.Data.Entities.Category;

public class Using : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Using(string name)
    {
        Name = name;
    }
    private Using() : this(null!)
    { }
}
