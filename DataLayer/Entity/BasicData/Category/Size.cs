namespace Entity.BasicData.Category;

public class Size(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    private Size() : this(null!)
    { }
}
