namespace Proprette.Domain.Data.Entities.Category;

public class FreeCodeC : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public FreeCodeC(string name)
    {
        Name = name;
    }
    private FreeCodeC() : this(null!)
    { }
}
