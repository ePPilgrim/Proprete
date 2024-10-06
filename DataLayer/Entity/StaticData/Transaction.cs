using Entity.BasicData;
using Entity.Enums;

namespace Entity.StaticData;

public class Transaction
{
    public int Id { get; set; }
    public TransactionCode TransactionCode { get; set; }
    public int HoldingId { get; set; }
    public Holding Holding { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public double Nominal { get; set; }
    public double Price { get; set; }
}
