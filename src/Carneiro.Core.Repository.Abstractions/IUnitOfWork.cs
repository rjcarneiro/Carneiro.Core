namespace Carneiro.Core.Repository.Abstractions;

/// <summary>
/// Unit of work.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IUnitOfWork : IDisposable
{
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
    /// <returns></returns>
    Task AddAsync<T>(T entity) where T : class, IAuditableEntity;

    /// <summary>
    /// Adds a range of <typeparamref name="T"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities">The entities.</param>
    /// <returns></returns>
    Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class, IAuditableEntity;

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
    /// Gets the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<List<T>> GetAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Queries this instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IQueryable<T> Query<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Queries the specified expression.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Queries the specified identifier.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    IQueryable<T> Query<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Queries the specified identifier.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    IQueryable<T> Query<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if the entity <typeparamref name="T"/> has any records.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<bool> AnyAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<bool> AnyAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> and an <paramref name="expression"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<bool> AnyAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> and an <paramref name="expression"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> AnyAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if <paramref name="expression"/> is true or false, asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Checks if <paramref name="expression"/> is true or false, asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<T> FirstAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T> FirstAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<T> FirstAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<T> FirstOrDefaultAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<T> FirstOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<T> FirstOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<T> SingleAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task<T> SingleAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<T> SingleAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<T> SingleOrDefaultAsync<T>(long id) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    Task<T> SingleOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<T> SingleOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Saves the context asynchronously.
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();

    /// <summary>
    /// Saves the context asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task SaveAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Executes the in transaction scope asynchronously.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="isolationLevel">The isolation level. Default is <see cref="System.Data.IsolationLevel.Serializable"/></param>
    /// <returns></returns>
    Task ExecuteInTransactionScopeAsync(Func<Task> action, IsolationLevel isolationLevel = IsolationLevel.Serializable);
}