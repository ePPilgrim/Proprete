namespace Proprette.Domain.Data.Internals
{
    public class SetCollection<T> : IDBCollection<T> where T : class
    {
        private readonly HashSet<T> values;
        private readonly Dictionary<string, IDBCollection<object>> properties;

        public SetCollection()
        {
            this.values = new HashSet<T> { };   
            properties = new Dictionary<string, IDBCollection<object>>();
        }

        public SetCollection(IEnumerable<T> values, ICompareKeys<T>? compareKeys) : this()
        { 
            this.values = (compareKeys != null) ? values.ToHashSet(compareKeys) : values.ToHashSet(); 
        }

        public virtual IEnumerable<T> Values => values;

        public virtual T? Find(object obj)
        {
            if (values.TryGetValue((T)obj, out T? res))
                return res;
            return default;
        }

        public virtual bool Remove(object obj)
        {
            return values.Remove((T)obj);   
        }

        public virtual IDBCollection<object>? GetProperty(string propertyName)
        {
            if(properties.TryGetValue(propertyName, out var res))
            {
                return res;
            }
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new InvalidDataException($"No property {propertyName} exsists for objects of type {nameof(T)}.");
            }

            properties[propertyName] = new SetCollection<object>(Values.Select(el => propertyInfo.GetValue(el)) as IEnumerable<object>, null);

            return properties[propertyName];
        }
    }
}
