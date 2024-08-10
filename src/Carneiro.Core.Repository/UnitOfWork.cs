using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Carneiro.Core.Repository;

/// <summary>
/// Working implementation of Entity Framework for <see cref="IUnitOfWork{TDbContext}"/>.
/// </summary>
/// <seealso cref="IUnitOfWork{TDbContext}" />
public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
{
    /// <summary>
    /// Gets the <typeparamref name="TDbContext"/>.
    /// </summary>
    protected TDbContext DbContext { get; }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    protected ILogger<UnitOfWork<TDbContext>> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork{TDbContext}" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UnitOfWork(TDbContext dbContext, ILogger<UnitOfWork<TDbContext>> logger)
    {
        DbContext = dbContext;
        Logger = logger;
    }

    /// <inheritdoc />
    public virtual Task<bool> CanConnectAsync() => DbContext.Database.CanConnectAsync();

    /// <inheritdoc />
    public virtual void Add<T>(T entity) where T : class, IAuditableEntity
    {
        AuditCreate(entity);
        DbContext.Add(entity);
    }

    /// <inheritdoc />
    public virtual async Task AddAsync<T>(T entity) where T : class, IAuditableEntity => await AddAsync<T>(entity, CancellationToken.None);

    /// <inheritdoc />
    public virtual async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class, IAuditableEntity
    {
        AuditCreate(entity);
        await DbContext.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public virtual void AddRange<T>(IEnumerable<T> entities) where T : class, IAuditableEntity
    {
        IEnumerable<T> iAuditableEntities = entities as T[] ?? entities.ToArray();

        foreach (T entity in iAuditableEntities)
            AuditCreate(entity);

        DbContext.AddRange(iAuditableEntities);
    }

    /// <inheritdoc />
    public virtual Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class, IAuditableEntity => AddRangeAsync<T>(entities, CancellationToken.None);

    /// <inheritdoc />
    public virtual Task AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class, IAuditableEntity
    {
        IEnumerable<T> iAuditableEntities = entities as T[] ?? entities.ToArray();
        foreach (T entity in iAuditableEntities)
            AuditCreate(entity);

        return DbContext.AddRangeAsync(iAuditableEntities, cancellationToken);
    }

    /// <inheritdoc />
    public virtual void Update<T>(T entity) where T : class, IAuditableEntity
    {
        AuditUpdate(entity);
        DbContext.Update(entity);
    }

    /// <inheritdoc />
    public virtual void UpdateRange<T>(IEnumerable<T> entities) where T : class, IAuditableEntity
    {
        IEnumerable<T> iAuditableEntities = entities as T[] ?? entities.ToArray();
        foreach (T entity in iAuditableEntities)
            AuditUpdate(entity);

        DbContext.UpdateRange(iAuditableEntities);
    }

    /// <inheritdoc />
    public virtual void Delete<T>(T entity) where T : class, IAuditableEntity => Delete(entity, hardDelete: false);

    /// <inheritdoc />
    public virtual void Delete<T>(T entity, bool hardDelete) where T : class, IAuditableEntity
    {
        if (hardDelete)
        {
            DbContext.Set<T>().Remove(entity);
        }
        else
        {
            AuditDelete(entity);
            DbContext.Update(entity);
        }
    }

    /// <inheritdoc />
    public virtual void Delete<T>(IEnumerable<T> entities) where T : class, IAuditableEntity => Delete(entities, hardDelete: false);

    /// <inheritdoc />
    public virtual void Delete<T>(IEnumerable<T> entities, bool hardDelete) where T : class, IAuditableEntity
    {
        if (hardDelete)
        {
            DbContext.Set<T>().RemoveRange(entities);
        }
        else
        {
            IEnumerable<T> baseEntities = entities.ToList();
            foreach (T entity in baseEntities)
            {
                AuditDelete(entity);
            }

            DbContext.UpdateRange(baseEntities);
        }
    }

    /// <inheritdoc />
    public virtual Task<List<T>> GetAsync<T>() where T : class, IAuditableEntity => GetAsync<T>(CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<List<T>> GetAsync<T>(CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().ToListAsync(cancellationToken: cancellationToken);

    /// <inheritdoc />
    public virtual Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => GetAsync<T>(expression, CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query(expression).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public virtual IQueryable<T> Query<T>() where T : class, IAuditableEntity => DbContext.Set<T>().Where(t => t.IsDeleted == false);

    /// <inheritdoc />
    public virtual IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => DbContext.Set<T>().Where(t => t.IsDeleted == false).Where(expression);

    /// <inheritdoc />
    public virtual IQueryable<T> Query<T>(long id) where T : class, IAuditableEntity => DbContext.Set<T>().Where(t => t.IsDeleted == false && t.Id == id);

    /// <inheritdoc />
    public virtual IQueryable<T> Query<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => DbContext.Set<T>().Where(t => t.IsDeleted == false && t.Id == id).Where(expression);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>() where T : class, IAuditableEntity => AnyAsync<T>(CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().AnyAsync(cancellationToken: cancellationToken);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(long id) where T : class, IAuditableEntity => AnyAsync<T>(id, CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(long id, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>(id).AnyAsync(t => t.Id == id, cancellationToken: cancellationToken);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>(id).AnyAsync(expression);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>(id).AnyAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>().AnyAsync(expression);

    /// <inheritdoc />
    public virtual Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().AnyAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> FirstAsync<T>(long id) where T : class, IAuditableEntity => Query<T>(id).FirstAsync();

    /// <inheritdoc />
    public virtual Task<T> FirstAsync<T>() where T : class, IAuditableEntity => Query<T>().FirstAsync();

    /// <inheritdoc />
    public virtual Task<T> FirstAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query(expression).FirstAsync();

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(long id) where T : class, IAuditableEntity => DbContext.Set<T>().FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted == false);

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>().FirstOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>(id).FirstOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().FirstOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> FirstOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity =>
        Query<T>(id).FirstOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> SingleAsync<T>(long id) where T : class, IAuditableEntity => Query<T>(id).SingleAsync();

    /// <inheritdoc />
    public virtual Task<T> SingleAsync<T>() where T : class, IAuditableEntity => Query<T>().SingleAsync();

    /// <inheritdoc />
    public virtual Task<T> SingleAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query(expression).SingleAsync();

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(long id) where T : class, IAuditableEntity => DbContext.Set<T>().SingleOrDefaultAsync(t => t.Id == id && t.IsDeleted == false);

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>().SingleOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>(id).SingleOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().SingleOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> SingleOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity =>
        Query<T>(id).SingleOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> LastAsync<T>(long id) where T : class, IAuditableEntity => Query<T>(id).LastAsync();

    /// <inheritdoc />
    public virtual Task<T> LastAsync<T>() where T : class, IAuditableEntity => Query<T>().LastAsync();

    /// <inheritdoc />
    public virtual Task<T> LastAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query(expression).LastAsync();

    /// <inheritdoc />
    public virtual Task<T> LastOrDefaultAsync<T>(long id) where T : class, IAuditableEntity => DbContext.Set<T>().LastOrDefaultAsync(t => t.Id == id && t.IsDeleted == false);

    /// <inheritdoc />
    public virtual Task<T> LastOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>().LastOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> LastOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query<T>(id).LastOrDefaultAsync(expression);

    /// <inheritdoc />
    public virtual Task<T> LastOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity => Query<T>().LastOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> LastOrDefaultAsync<T>(long id, Expression<Func<T, bool>> expression, CancellationToken cancellationToken) where T : class, IAuditableEntity =>
        Query<T>(id).LastOrDefaultAsync(expression, cancellationToken);

    /// <inheritdoc />
    public virtual Task<int> CountAsync<T>() where T : class, IAuditableEntity => Query<T>().CountAsync();

    /// <inheritdoc />
    public virtual Task<int> CountAsync<T>(Expression<Func<T, bool>> expression) where T : class, IAuditableEntity => Query(expression).CountAsync();

    /// <inheritdoc />
    public virtual Task<decimal> SumAsync<T>(Expression<Func<T, decimal>> expression) where T : class, IAuditableEntity => Query<T>().SumAsync(expression);

    /// <inheritdoc />
    public virtual Task<decimal?> SumAsync<T>(Expression<Func<T, decimal?>> expression) where T : class, IAuditableEntity => Query<T>().SumAsync(expression);


    /// <inheritdoc />
    public virtual Task<int> SumAsync<T>(Expression<Func<T, int>> expression) where T : class, IAuditableEntity => Query<T>().SumAsync(expression);

    /// <inheritdoc />
    public virtual Task<int?> SumAsync<T>(Expression<Func<T, int?>> expression) where T : class, IAuditableEntity => Query<T>().SumAsync(expression);

    /// <inheritdoc />
    public virtual Task SaveAsync() => DbContext.SaveChangesAsync();

    /// <inheritdoc />
    public virtual Task SaveAsync(CancellationToken cancellationToken) => DbContext.SaveChangesAsync(cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> ExecuteStoredProcedureAsync<T>(string sql, Func<DbDataReader, Task<T>> action) where T : class =>
        ExecuteStoredProcedureAsync(sql, sqlParameters: new Dictionary<string, object>(), CommandBehavior.Default, action, CancellationToken.None);

    /// <inheritdoc />
    public Task<T> ExecuteStoredProcedureAsync<T>(string sql, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken) where T : class =>
        ExecuteStoredProcedureAsync(sql, new Dictionary<string, object>(), CommandBehavior.Default, action, cancellationToken);

    /// <inheritdoc />
    public Task<T> ExecuteStoredProcedureAsync<T>(string sql, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action) where T : class =>
        ExecuteStoredProcedureAsync(sql, new Dictionary<string, object>(), commandBehavior, action, CancellationToken.None);

    /// <inheritdoc />
    public Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, Func<DbDataReader, Task<T>> action) where T : class =>
        ExecuteStoredProcedureAsync(sql, sqlParameters, CommandBehavior.Default, action, CancellationToken.None);

    /// <inheritdoc />
    public Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken) where T : class =>
        ExecuteStoredProcedureAsync(sql, sqlParameters, CommandBehavior.Default, action, cancellationToken);

    /// <inheritdoc />
    public Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action) where T : class =>
        ExecuteStoredProcedureAsync(sql, sqlParameters, commandBehavior, action, CancellationToken.None);

    /// <inheritdoc />
    public virtual async Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken)
        where T : class
    {
        Logger.LogInformation("Building new {StoredProcedure} with sql {Sql}", nameof(CommandType.StoredProcedure), sql);

        await using DbConnection connection = DbContext.Database.GetDbConnection();
        await using DbCommand command = connection.CreateCommand();

        command.CommandText = sql;
        command.CommandType = CommandType.StoredProcedure;

        if (sqlParameters.Count > 0)
        {
            Logger.LogInformation("Adding {SqlParametersCount} parameters to the database command", sqlParameters.Count);

            foreach (KeyValuePair<string, object> sqlParameter in sqlParameters)
            {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = sqlParameter.Key;
                parameter.Value = sqlParameter.Value;

                Logger.LogInformation("Parameter: {ParameterParameterName} Value: {ParameterValue}", parameter.ParameterName, parameter.Value);

                command.Parameters.Add(parameter);
            }
        }

        Logger.LogInformation("Executing sql '{Sql}' as '{CommandCommandType}'", sql, command.CommandType);

        await connection.OpenAsync(cancellationToken);
        await using DbDataReader dbDataReader = await command.ExecuteReaderAsync(commandBehavior, cancellationToken);
        return await action(dbDataReader);
    }

    /// <inheritdoc />
    public virtual async Task<StoreProcedureResult<T>> ExecuteStoredProcedureAsync<S, T>(string sql, IEnumerable<S> sqlParameters, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action,
        CancellationToken cancellationToken)
        where S : DbParameter
        where T : class
    {
        Logger.LogInformation("Building new {StoredProcedure} with sql {Sql}", nameof(CommandType.StoredProcedure), sql);

        await using DbConnection connection = DbContext.Database.GetDbConnection();
        await using DbCommand command = connection.CreateCommand();

        command.CommandText = sql;
        command.CommandType = CommandType.StoredProcedure;

        S[] dbParameters = sqlParameters as S[] ?? sqlParameters.ToArray();

        if (dbParameters.Length != 0)
        {
            Logger.LogInformation("Adding {SqlParametersCount} parameters to the database command", dbParameters.Length);
            command.Parameters.AddRange(dbParameters);
        }

        Logger.LogInformation("Executing sql '{Sql}' as '{CommandCommandType}'", sql, command.CommandType);

        await connection.OpenAsync(cancellationToken);
        command.Transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        try
        {
            await using DbDataReader dbDataReader = await command.ExecuteReaderAsync(commandBehavior, cancellationToken);
            T result = await action(dbDataReader);
            await dbDataReader.CloseAsync();

            await command.Transaction.CommitAsync(cancellationToken);

            var model = new StoreProcedureResult<T>
            {
                Result = result,
                Output = command.Parameters.Cast<SqlParameter>()
                    .Where(p => p.Direction is ParameterDirection.Output or ParameterDirection.InputOutput)
                    .ToDictionary(p => p.ParameterName, p => p.Value),
            };

            return model;
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Unable to process Stored Procedure '{Sql}'", sql);
            await command.Transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    /// <inheritdoc />
    public virtual void Dispose()
    {
        if (DbContext is IDisposable contextDisposable)
        {
            contextDisposable.Dispose();
        }
        else if (DbContext != null)
        {
            _ = DbContext.DisposeAsync().AsTask();
        }
    }

    /// <inheritdoc />
    public virtual async ValueTask DisposeAsync()
    {
        if (DbContext != null)
        {
            await DbContext.DisposeAsync();
        }
    }

    private void AuditCreate<T>(T entity) where T : IAuditableEntity
    {
        entity.IsActive = true;
        entity.IsDeleted = false;
        entity.CreateDate = DateTime.UtcNow;
        entity.UpdateDate = null;
        entity.DeleteDate = null;
    }

    private void AuditUpdate<T>(T entity) where T : IAuditableEntity => entity.UpdateDate = DateTime.UtcNow;

    private void AuditDelete<T>(T entity) where T : IAuditableEntity
    {
        entity.IsActive = false;
        entity.IsDeleted = true;
        entity.DeleteDate = DateTime.UtcNow;
    }
}