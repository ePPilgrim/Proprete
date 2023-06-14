using Proprette.Domain.Data.Internals;
using Proprette.Domain.Services.DataSeeding.Internal;

namespace Proprette.Domain.Services.DataSeeding
{
    public interface IEntityFactory<T> where T : class
    {
        public IDBCollection<T> CreateCollectionShallow(IEnumerable<T> values);
        public IDBCollection<T> CreateCollectionDeep(IEnumerable<T> values);
        public ICompareKeys<T> CreateComparer();
        public IPopulateTableInternal<T> CreatePopulateInternal();
    }
}
