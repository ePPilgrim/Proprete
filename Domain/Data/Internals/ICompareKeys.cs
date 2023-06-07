namespace Proprette.Domain.Data.Internals;

internal interface ICompareKeys<T>
{
    public int CompareInternalKey(T other);
    public int CompareAltKeys(T other);
}
