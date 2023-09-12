namespace Proprette.Domain.Data.Entities.Category;

internal class Composition : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Composition(string name)
    {
        Name = name;
    }
    public Composition() : this(null!)
    { }
}
