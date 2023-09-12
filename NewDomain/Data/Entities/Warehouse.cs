namespace Proprette.NewDomain.Data.Entities;

public class Warehouse
{
    public int Id { get; set; } 
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }
}
