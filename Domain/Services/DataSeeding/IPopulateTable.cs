namespace Proprette.Domain.Services.DataSeeding;

public interface IPopulateTable<in T> where T : class
{
    Task UpdateOrInsert(IEnumerable<T> records);
    Task Insert(IEnumerable<T> records);
    Task Delete();
}
