namespace Carneiro.Core.Repository.Abstractions;

/// <summary>
/// The Unit of work abstraction.
/// </summary>
/// <seealso cref="IDisposable" />
public interface IUnitOfWork<TDbContext> : IDisposable, IAsyncDisposable
    where TDbContext : DbContext
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
    IQueryable<T> Query<T>(int id) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Queries the specified identifier.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    IQueryable<T> Query<T>(int id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity, IEntity;

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
    Task<bool> AnyAsync<T>(int id) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> asynchronously.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    Task<bool> AnyAsync<T>(int id, CancellationToken cancellationToken) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> and an <paramref name="expression"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    Task<bool> AnyAsync<T>(int id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Checks if exists any record based on <paramref name="id"/> and an <paramref name="expression"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<bool> AnyAsync<T>(int id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity, IEntity;

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
    Task<T> FirstAsync<T>(int id) where T : class, IAuditableEntity, IEntity;

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
    Task<T> FirstOrDefaultAsync<T>(int id) where T : class, IAuditableEntity, IEntity;

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
    Task<T> FirstOrDefaultAsync<T>(int id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> FirstOrDefaultAsync<T>(int id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Firsts the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Gets a single entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    Task<T> SingleAsync<T>(int id) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Gets a single entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    Task<T> SingleAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Gets a single entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<T> SingleAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    Task<T> SingleOrDefaultAsync<T>(int id) where T : class, IAuditableEntity, IEntity;

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
    Task<T> SingleOrDefaultAsync<T>(int id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> SingleOrDefaultAsync<T>(int id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Singles the or default asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the last entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    Task<T> LastAsync<T>(int id) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Gets the last entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    Task<T> LastAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the last entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<T> LastAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the last or default entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    Task<T> LastOrDefaultAsync<T>(int id) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Gets the last or default entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<T> LastOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Gets the last or default entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    Task<T> LastOrDefaultAsync<T>(int id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Gets the last or default entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> LastOrDefaultAsync<T>(int id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity, IEntity;

    /// <summary>
    /// Gets the last or default entity asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<T> LastOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity;

    /// <summary>
    /// Counts the number of entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    Task<int> CountAsync<T>() where T : class, IAuditableEntity;

    /// <summary>
    /// Counts the number of entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<int> CountAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Sums an <paramref name="expression"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<decimal> SumAsync<T>(Expression<Func<T, decimal>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Sums an <paramref name="expression"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<decimal?> SumAsync<T>(Expression<Func<T, decimal?>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Sums an <paramref name="expression"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<int> SumAsync<T>(Expression<Func<T, int>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Sums an <paramref name="expression"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    Task<int?> SumAsync<T>(Expression<Func<T, int?>> expression) where T : class, IAuditableEntity;

    /// <summary>
    /// Saves the context asynchronously.
    /// </summary>
    Task SaveAsync();

    /// <summary>
    /// Saves the context asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task SaveAsync(CancellationToken cancellationToken);
}