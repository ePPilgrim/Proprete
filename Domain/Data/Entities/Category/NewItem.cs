using System.Runtime;

namespace Proprette.Domain.Data.Entities.Category;

public class NewItem
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public int ColorId { get; set; }
    public Color Color { get; set; } = null!;
}
