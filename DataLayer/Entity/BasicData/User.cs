namespace Proprette.DataLayer.Entity.BasicData;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}
