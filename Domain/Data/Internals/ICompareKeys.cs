namespace Proprette.Domain.Data.Internals;

public interface ICompareKeys<T> : IComparer<T>, IEqualityComparer<T>
{
}
