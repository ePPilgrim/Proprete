using Proprette.DataLayer.Entity.BasicData;
using Proprette.DataLayer.Entity.Enums;

namespace Proprette.DataLayer.Entity.StaticData;

public class Transaction
{
    public int Id { get; set; }
    public TransactionCode TransactionCode { get; set; }
    public int HoldingId { get; set; }
    public Holding Holding { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateOnly Date { get; set; }
    public double Nominal { get; set; }
    public double Price { get; set; }
}
