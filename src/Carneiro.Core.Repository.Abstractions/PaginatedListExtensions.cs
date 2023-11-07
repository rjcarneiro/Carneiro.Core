namespace Carneiro.Core.Repository.Abstractions;

/// <summary>
/// Extensions for <see cref="PaginatedList{T}"/>.
/// </summary>
public static class PaginatedListExtensions
{
    /// <summary>
    /// Converts a specified <paramref name="paginatedList"/> into a <see cref="PaginatedList{T}"/> of <paramref name="items"/>.
    /// </summary>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="paginatedList">The paginated list.</param>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    public static PaginatedList<T> Convert<Y, T>(this PaginatedList<Y> paginatedList, IEnumerable<T> items)
        => new(items, paginatedList.PageIndex, paginatedList.TotalPages);
}