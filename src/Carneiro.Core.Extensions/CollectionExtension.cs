using System;
using System.Collections.Generic;
using System.Linq;

namespace Carneiro.Core.Extensions;

/// <summary>
/// Extensions for collections
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// Gets a random element from <paramref name="list"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list.</param>
    /// <returns></returns>
    public static T RandomElement<T>(this IList<T> list) => list[Random.Shared.Next(list.Count)];

    /// <summary>
    /// Gets a random element from <paramref name="array"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array">The array.</param>
    /// <returns></returns>
    public static T RandomElement<T>(this T[] array) => array[Random.Shared.Next(array.Length)];

    /// <summary>
    /// Splits the <paramref name="source"/> into small lists of <paramref name="chunkSize"/> elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source.</param>
    /// <param name="chunkSize">Size of the chunk.</param>
    /// <returns></returns>
    public static List<List<T>> ChunkBy<T>(this IList<T> source, int chunkSize) => source
        .Select((x, i) => new { Index = i, Value = x })
        .GroupBy(x => x.Index / chunkSize)
        .Select(x => x.Select(v => v.Value).ToList())
        .ToList();
}