using System;


public static class StringExtensions
{
    public static Guid ToGuid(this string value)
    {
        if (Guid.TryParse(value, out Guid result)) { return result; }
        return Guid.Empty;
    }
}
