namespace Proprette.Domain.Data.Internals
{
    public interface IDBCollection<out T>
    {
        public IEnumerable<T> Values { get; }
        public T? Find(object obj);
        public bool Remove(object obj);
        public IDBCollection<object>? GetProperty(string propertyName);
    }
}
