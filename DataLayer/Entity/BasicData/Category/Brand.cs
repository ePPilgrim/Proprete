namespace Entity.BasicData.Category;

public class Brand(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    private Brand() : this(null!)
    { }
}
