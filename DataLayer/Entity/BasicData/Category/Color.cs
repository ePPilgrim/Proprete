namespace Entity.BasicData.Category;

public class Color(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    private Color() : this(null!)
    { }
}