using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Carneiro.Core.Repository;

/// <summary>
/// Working implementation of Entity Framework for <see cref="IStoredProcedureUnitOfWork{TDbContext}"/>.
/// </summary>
/// <seealso cref="IUnitOfWork{TDbContext}" />
public class StoredProcedureUnitOfWork<TDbContext> : IStoredProcedureUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    /// <summary>
    /// Gets the <typeparamref name="TDbContext"/>.
    /// </summary>
    protected TDbContext DbContext { get; }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    protected ILogger<StoredProcedureUnitOfWork<TDbContext>> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StoredProcedureUnitOfWork{TDbContext}" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="logger">The logger.</param>
    public StoredProcedureUnitOfWork(TDbContext dbContext, ILogger<StoredProcedureUnitOfWork<TDbContext>> logger)
    {
        DbContext = dbContext;
        Logger = logger;
    }

    /// <inheritdoc />
    public virtual Task<T> ExecuteStoredProcedureAsync<T>(string sql, Func<DbDataReader, Task<T>> action) where T : class =>
        ExecuteStoredProcedureAsync(sql, sqlParameters: new Dictionary<string, object>(), CommandBehavior.Default, action, CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<T> ExecuteStoredProcedureAsync<T>(string sql, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken) where T : class =>
        ExecuteStoredProcedureAsync(sql, new Dictionary<string, object>(), CommandBehavior.Default, action, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> ExecuteStoredProcedureAsync<T>(string sql, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action) where T : class =>
        ExecuteStoredProcedureAsync(sql, new Dictionary<string, object>(), commandBehavior, action, CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, Func<DbDataReader, Task<T>> action) where T : class =>
        ExecuteStoredProcedureAsync(sql, sqlParameters, CommandBehavior.Default, action, CancellationToken.None);

    /// <inheritdoc />
    public virtual Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken) where T : class =>
        ExecuteStoredProcedureAsync(sql, sqlParameters, CommandBehavior.Default, action, cancellationToken);

    /// <inheritdoc />
    public virtual Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action) where T : class =>
        ExecuteStoredProcedureAsync(sql, sqlParameters, commandBehavior, action, CancellationToken.None);

    /// <inheritdoc />
    public virtual async Task<T> ExecuteStoredProcedureAsync<T>(string sql, Dictionary<string, object> sqlParameters, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action, CancellationToken cancellationToken)
        where T : class
    {
        Logger.LogInformation("Building new {StoredProcedure} with sql {Sql}", nameof(CommandType.StoredProcedure), sql);

        DbConnection connection = DbContext.Database.GetDbConnection();
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
        try
        {
            await using DbDataReader dbDataReader = await command.ExecuteReaderAsync(commandBehavior, cancellationToken);
            return await action(dbDataReader);
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    /// <inheritdoc />
    public virtual async Task<StoreProcedureResult<T>> ExecuteStoredProcedureAsync<S, T>(string sql, IEnumerable<S> sqlParameters, CommandBehavior commandBehavior, Func<DbDataReader, Task<T>> action,
        CancellationToken cancellationToken)
        where S : DbParameter
        where T : class
    {
        Logger.LogInformation("Building new {StoredProcedure} with sql {Sql}", nameof(CommandType.StoredProcedure), sql);

        DbConnection connection = DbContext.Database.GetDbConnection();
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
        finally
        {
            await connection.CloseAsync();
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
}