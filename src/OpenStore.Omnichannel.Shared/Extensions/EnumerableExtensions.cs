// ReSharper disable once CheckNamespace

namespace System.Collections.Generic;

public static class EnumerableExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.IsNotEmpty();

    public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable) => enumerable.Any();
    
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null)
            throw new ArgumentNullException(nameof (source));
        if (action == null)
            throw new ArgumentNullException(nameof (action));
        foreach (var obj in source)
        {
            action(obj);
            yield return obj;
        }
    }

}