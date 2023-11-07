using Microsoft.EntityFrameworkCore;

namespace Carneiro.Core.Repository.Abstractions;

/// <summary>
/// Paginated list class.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="System.Collections.Generic.List{T}" />
public class PaginatedList<T> : List<T>
{
    /// <summary>
    /// Gets the index of the page.
    /// </summary>
    /// <value>
    /// The index of the page.
    /// </value>
    public int PageIndex { get; }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    /// <value>
    /// The total pages.
    /// </value>
    public int TotalPages { get; }

    /// <summary>
    /// Gets the next page.
    /// </summary>
    /// <value>
    /// The next page.
    /// </value>
    public int NextPage => PageIndex + 1;

    /// <summary>
    /// Gets the previous page.
    /// </summary>
    /// <value>
    /// The previous page.
    /// </value>
    public int PreviousPage => PageIndex - 1;

    /// <summary>
    /// Gets a value indicating whether this instance has previous page.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
    /// </value>
    public bool HasPreviousPage => PageIndex > 1;

    /// <summary>
    /// Gets a value indicating whether this instance has next page.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has next page; otherwise, <c>false</c>.
    /// </value>
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
    /// </summary>
    /// <param name="items">The items.</param>
    /// <param name="count">The count.</param>
    /// <param name="pageIndex">Index of the page.</param>
    /// <param name="pageSize">Size of the page.</param>
    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
    /// </summary>
    /// <param name="items">The items.</param>
    /// <param name="pageIndex">Index of the page.</param>
    /// <param name="totalPages">The total pages.</param>
    public PaginatedList(IEnumerable<T> items, int pageIndex, int totalPages)
    {
        PageIndex = pageIndex;
        TotalPages = totalPages;

        AddRange(items);
    }

    /// <summary>
    /// Creates the paginated list asynchronously.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="pageIndex">Index of the page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <returns></returns>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        List<T> items = await source.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}