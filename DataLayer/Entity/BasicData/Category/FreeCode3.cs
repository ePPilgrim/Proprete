namespace Entity.BasicData.Category;

public class FreeCode3(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    private FreeCode3() : this(null!)
    { }
}
