using Proprette.Domain.Data.Internals;

namespace Proprette.Domain.Services.DataSeeding.Internal
{
    public interface IPopulateTableInternal<T> where T : class
    {
        Task UpdateOrInsert(IDBCollection<T> records);
        Task Insert(IDBCollection<T> records);
        Task Delete();
    }
}
