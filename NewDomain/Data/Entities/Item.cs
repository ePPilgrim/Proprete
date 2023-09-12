using System.Runtime;
using Proprette.Domain.Data.Entities.Category;

namespace Proprette.NewDomain.Data.Entities;

public class Item
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public int ColorId { get; set; }
    public Color Color { get; set; } = null!;
}
