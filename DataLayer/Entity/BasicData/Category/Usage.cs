namespace Entity.BasicData.Category;

public class Usage(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    private Usage() : this(null!)
    { }
}
