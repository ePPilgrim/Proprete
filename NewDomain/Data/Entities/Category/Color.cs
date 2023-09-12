namespace Proprette.Domain.Data.Entities.Category;

public class Color : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Color(string name)
    {
        Name = name;
    }
    private Color() : this(null!)
    { }
}