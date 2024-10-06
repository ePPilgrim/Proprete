namespace Entity.BasicData.Category;

public class Using(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    private Using() : this(null!)
    { }
}
