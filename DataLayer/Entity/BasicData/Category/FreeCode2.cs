namespace Entity.BasicData.Category;

public class FreeCode2(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    private FreeCode2() : this(null!)
    { }
}
