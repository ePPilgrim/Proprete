using Proprette.Domain.Data.Common;

namespace Proprette.Domain.Data.Internals;

internal static class CompareHelper
{
    internal static int Compare(string key1, ItemType key2, string otherKey1, ItemType otherKey2)
    {
        int key1Cmp = string.Compare(key1, otherKey1);
        if (key1Cmp != 0) return key1Cmp;
        return key2.CompareTo(otherKey2);
    }

    internal static int Compare(int key1, DateTime key2, int otherKey1, DateTime otherKey2)
    {
        int key1Cmp = key1 - otherKey1;
        if (key1Cmp != 0) return key1Cmp;
        return key2.CompareTo(otherKey2);
    }

    internal static int Compare(string key1, ItemType key2, DateTime key3, string otherKey1, ItemType otherKey2, DateTime otherKey3)
    {
        int key12Cmp = Compare(key1, key2, otherKey1, otherKey2);
        if (key12Cmp != 0) return key12Cmp;
        return key3.CompareTo(otherKey3);
    }
}
