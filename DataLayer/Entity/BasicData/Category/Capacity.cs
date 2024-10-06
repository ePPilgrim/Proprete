namespace Entity.BasicData.Category;

public class Capacity(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    private Capacity() : this(null!)
    { }
}
