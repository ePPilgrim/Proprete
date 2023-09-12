namespace Proprette.Domain.Data.Entities.Category;

public class Usage : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Usage(string name)
    {
        Name = name;
    }
    private Usage() : this(null!)
    { }
}
