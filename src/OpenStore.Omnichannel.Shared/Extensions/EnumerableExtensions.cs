// ReSharper disable once CheckNamespace

namespace System.Collections.Generic;

public static class EnumerableExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.IsNotEmpty();

    public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable) => enumerable.Any();
}