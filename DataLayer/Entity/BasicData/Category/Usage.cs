namespace Proprette.DataLayer.Entity.BasicData.Category;

public class Usage(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public Usage() : this(null!)
    { }
}
