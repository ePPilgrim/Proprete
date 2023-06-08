using Proprette.Domain.Data.Common;

namespace Proprette.Domain.Data.Internals;

internal static class CompareHelper
{
    public static int Compare(string key1, ItemType key2, string otherKey1, ItemType otherKey2)
    {
        int key1Cmp = string.Compare(key1, otherKey1);
        int key2Cmp = key2.CompareTo(otherKey2);
        return compare(key1Cmp, key2Cmp);
    }

    public static int Compare(int key1, DateTime key2, int otherKey1, DateTime otherKey2)
    {
        int key1Cmp = key1 - otherKey1;
        int key2Cmp = key2.CompareTo(otherKey2);
        return compare(key1Cmp, key2Cmp);
    }

    private static int compare(int key1Cmp, int key2Cmp)
    {
        if (key1Cmp == 0 && key2Cmp == 0) return 0;
        if (key1Cmp < 0) return -1;
        if (key1Cmp > 0) return 1;
        if (key2Cmp < 0) return -1;
        return 1;
    }
}
