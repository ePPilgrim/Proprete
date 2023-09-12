namespace Proprette.Domain.Data.Entities.Category;

public class FreeCodeA : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public FreeCodeA(string name)
    {
        Name = name;
    }
    private FreeCodeA() : this(null!)
    { }
}
