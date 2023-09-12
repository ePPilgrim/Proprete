namespace Proprette.Domain.Data.Entities.Category;

public class FreeCodeB : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public FreeCodeB(string name)
    {
        Name = name;
    }
    private FreeCodeB() : this(null!)
    { }
}
