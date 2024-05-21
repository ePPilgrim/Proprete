namespace Proprette.DataLayer.Entity;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public string Comments { get; set; }   
}
