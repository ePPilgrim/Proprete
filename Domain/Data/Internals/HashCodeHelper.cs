using Proprette.Domain.Data.Common;

namespace Proprette.Domain.Data.Internals;

static internal class HashCodeHelper
{
    static public int Get(string a1, ItemType a2)
    {
        return new { name = a1, type = a2 }.GetHashCode();
    }

    internal static int Get(int itemID, DateTime dateTime)
    {
        return new {id = itemID, dateTime = dateTime}.GetHashCode();
    }
}
