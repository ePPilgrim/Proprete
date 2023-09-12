namespace Proprette.Domain.Data.Entities.Category;

internal class ItemType : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ItemType(string name)
    {
        Name = name;
    }
    public ItemType() : this(null!)
    { }
}
