namespace Proprette.Domain.Data.Entities.Category;

public class Size : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Size(string name)
    {
        Name = name;
    }
    private Size() : this(null!)
    { }
}
