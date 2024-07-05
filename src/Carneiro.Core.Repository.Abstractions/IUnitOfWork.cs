using System.Data.Common;

namespace Carneiro.Core.Repository.Abstractions;

/// <summary>
/// Unit of work.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Checks either it can connect to the database asynchronously.
    /// </summary>
    Task<bool> CanConnectAsync();

    /// <summary>
    /// Adds the specified entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The entity.</param>
    void Add<T>(T entity) where T : class, IAuditableEntity;

    /// <summary>
    /// Adds a range of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities">The entities.</param>
    void AddRange<T>(IEnumerable<T> entities) where T : class, IAuditableEntity;

    /// <summary>
    /// Adds the specified entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The entity.</param>
    Task AddAsync<T>(T entity) where T : class, IAuditableEntity;

    /// <summary>
    /// Adds the specified entity asynchronously.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Adds a range of <typeparamref name="T"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities">The entities.</param>
    Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class, IAuditableEntity;

    /// <summary>
    /// Adds a range of <typeparamref name="T"/> asynchronously.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    Task AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The entity.</param>
    void Update<T>(T entity) where T : class, IAuditableEntity;

    /// <summary>
    /// Updates a range of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities">The entities.</param>
    void UpdateRange<T>(IEnumerable<T> entities) where T : class, IAuditableEntity;

    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The entity.</param>
    void Delete<T>(T entity) where T : class, IAuditableEntity;

    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity">The entity.</param>
    /// <param name="hardDelete">if set to <c>true</c> [hard delete].</param>
    void Delete<T>(T entity, bool hardDelete) where T : class, IAuditableEntity;

    /// <summary>
    /// Deletes the specified entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities">The entities.</param>
    void Delete<T>(IEnumerable<T> entities) where T : class, IAuditableEntity;

    /// <summary>
    /// Deletes the specified entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities">The entities.</param>
    /// <param name="hardDelete">if set to <c>true</c> [hard delete].</param>
    void Delete<T>(IEnumerable<T> entities, bool hardDelete) where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the entities asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    Task<List<T>> GetAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the entities asynchronously.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    Task<List<T>> GetAsync<T>(CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Queries this instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    IQueryable<T> Query<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Queries the specified expression.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Queries the specified identifier.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    IQueryable<T> Query<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Queries the specified identifier.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    IQueryable<T> Query<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if the entity <typeparamref name="T"/> has any records.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    Task<bool> AnyAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if the entity <typeparamref name="T"/> has any records.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    Task<bool> AnyAsync<T>(CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    Task<bool> AnyAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> asynchronously.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    Task<bool> AnyAsync<T>(long id, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> and an <paramref name="expression"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    Task<bool> AnyAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> and an <paramref name="expression"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<bool> AnyAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if <paramref name="expression"/> is true or false, asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if <paramref name="expression"/> is true or false, asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    Task<T> FirstAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    Task<T> FirstAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<T> FirstAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    Task<T> FirstOrDefaultAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    Task<T> FirstOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> FirstOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    Task<T> SingleAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    Task<T> SingleAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<T> SingleAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    Task<T> SingleOrDefaultAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    Task<T> SingleOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> SingleOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Saves the context asynchronously.
    /// </summary>
    Task SaveAsync();

    /// <summary>
    /// Saves the context asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task SaveAsync(CancellationToken cancellationToken);

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