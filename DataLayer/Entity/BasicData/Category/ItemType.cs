namespace Entity.BasicData.Category;

public class ItemType(string name) : ICategory
{
    public int Id { get; set; }
    public required string Name { get; set; } = name;

    public ItemType() : this(null!)
    { }
}
