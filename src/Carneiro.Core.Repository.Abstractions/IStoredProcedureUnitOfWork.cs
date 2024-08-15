using System.Data.Common;

namespace Carneiro.Core.Repository.Abstractions;

/// <summary>
/// The Unit Of Work Stored Procedure Abstractions
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public interface IStoredProcedureUnitOfWork<TDbContext> : IDisposable, IAsyncDisposable
    where TDbContext : DbContext
{
    /// <summary>
    /// Executes a <see cref="CommandType.StoredProcedure"/> based on <paramref name="sql"/> asynchronously.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    Task<T> ExecuteStoredProcedureAsync<T>(string sql, Func<DbDataReader, Task<T>> action)
        where T : class;

    /// <summary>
    /// Executes a <see cref="CommandType.StoredProcedure"/> based on <paramref name="sql"/> asynchronously.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    Task<T> ExecuteStoredProcedureAsync<T>(string sql, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken)
        where T : class;

    /// <summary>
    /// Executes a <see cref="CommandType.StoredProcedure"/> based on <paramref name="sql"/> asynchronously.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="commandBehavior"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    Task<T> ExecuteStoredProcedureAsync<T>(string sql, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action)
        where T : class;

    /// <summary>
    /// Executes a <see cref="CommandType.StoredProcedure"/> based on <paramref name="sql"/> asynchronously.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="sqlParameters"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, Func<DbDataReader, Task<T>> action)
        where T : class;

    /// <summary>
    /// Executes a <see cref="CommandType.StoredProcedure"/> based on <paramref name="sql"/> asynchronously.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="sqlParameters"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken)
        where T : class;

    /// <summary>
    /// Executes a <see cref="CommandType.StoredProcedure"/> based on <paramref name="sql"/> asynchronously.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="sqlParameters"></param>
    /// <param name="commandBehavior"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action)
        where T : class;

    /// <summary>
    /// Executes a <see cref="CommandType.StoredProcedure"/> based on <paramref name="sql"/> asynchronously.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="sqlParameters"></param>
    /// <param name="commandBehavior"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken)
        where T : class;

    /// <summary>
    /// Executes a <see cref="CommandType.StoredProcedure"/> based on <paramref name="sql"/> with <paramref name="sqlParameters"/> under <paramref name="commandBehavior"/>.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="sqlParameters"></param>
    /// <param name="commandBehavior"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="T"></typeparam>
    Task<StoreProcedureResult<T>> ExecuteStoredProcedureAsync<S, T>(string sql, IEnumerable<S> sqlParameters, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken)
        where S : DbParameter
        where T : class;
}