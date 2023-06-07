using System.Diagnostics.CodeAnalysis;

namespace Proprette.Domain.Data.Internals;

internal class ComparerAltKeys<T> : IComparer<T>, IEqualityComparer<T> where T : ICompareKeys<T>
{
    public int Compare(T? obj1, T? obj2)
    {
        if (obj1 == null) 
        {
            throw new ArgumentNullException(nameof(obj1));
        }
        if (obj2 == null)
        {
            throw new ArgumentNullException(nameof(obj2));
        }
        return obj1.CompareAltKeys(obj2);
    }

    public bool Equals(T? obj1, T? obj2)
    {
        if (obj1 == null)
        {
            throw new ArgumentNullException(nameof(obj1));
        }
        if (obj2 == null)
        {
            throw new ArgumentNullException(nameof(obj2));
        }
        return obj1.CompareAltKeys(obj2) == 0;
    }

    public int GetHashCode([DisallowNull] T obj)
    {
        return obj.GetHashCode();
    }
}
