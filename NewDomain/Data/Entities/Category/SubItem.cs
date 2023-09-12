namespace Proprette.Domain.Data.Entities.Category;

internal class SubItem : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public SubItem(string name)
    {
        Name = name;
    }
    public SubItem() : this(null!)
    { }
}
