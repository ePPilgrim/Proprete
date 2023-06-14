using System.Diagnostics.CodeAnalysis;

namespace Proprette.Domain.Data.Internals;

public class CompareKeys<T> : ICompareKeys<T> where T : IComparer<T>
{
    public int Compare(T? obj1, T? obj2)
    {
        _ = obj1 ?? throw new ArgumentNullException(nameof(obj1));
        _ = obj2 ?? throw new ArgumentNullException(nameof(obj2));
        return obj1.Compare(obj1,obj2);
    }

    public bool Equals(T? obj1, T? obj2)
    {
        _ = obj1 ?? throw new ArgumentNullException(nameof(obj1));
        _ = obj2 ?? throw new ArgumentNullException(nameof(obj2));
        return obj1.Equals(obj2);
    }

    public int GetHashCode([DisallowNull] T obj)
    {
        return obj.GetHashCode();
    }
}


