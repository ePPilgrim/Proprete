namespace Proprette.Domain.Data.Entities.Category;

internal class Unit : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Unit(string name)
    {
        Name = name;
    }
    public Unit() : this(null!)
    { }
}
