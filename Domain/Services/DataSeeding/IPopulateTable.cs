namespace Proprette.Domain.Services.DataSeeding;

public interface IPopulateTable
{
    Task UpdateOrInsert();
    Task Delete();
}
