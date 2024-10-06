namespace Entity.BasicData.Category;

public class FreeCode1(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    private FreeCode1() : this(null!)
    { }
}
