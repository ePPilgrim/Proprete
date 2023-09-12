namespace Proprette.Domain.Data.Entities.Category;

public class Brand : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Brand(string name)
    {
        Name = name;
    }
    private Brand() : this(null!)
    {}
}
