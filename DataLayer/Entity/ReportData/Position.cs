using Proprette.DataLayer.Entity.StaticData;

namespace Proprette.DataLayer.Entity.ReportData;

public class Position
{
    public int Id { get; set; }
    public int HoldingId { get; set; }
    public Holding Holding { get; set; } = null!;
    public DateOnly Date { get; set; }
    public double BalanceNominal { get; set; }
}
