namespace Proprette.Domain.Data.Entities.Category;

public class Capacity : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Capacity(string name)
    {
        Name = name;
    }
    private Capacity() : this(null!)
    { }
}
