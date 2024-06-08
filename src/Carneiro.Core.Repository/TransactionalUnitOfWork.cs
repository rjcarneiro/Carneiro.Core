namespace Carneiro.Core.Repository;

/// <summary>
/// The unit of work transactional implementation.
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public class TransactionalUnitOfWork<TDbContext> : UnitOfWork<TDbContext>, ITransactionalUnitOfWork where TDbContext : DbContext
{
    /// <summary>
    /// Gets the <see cref="IServiceProvider"/>.
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <inheritdoc />
    public TransactionalUnitOfWork(TDbContext dbContext, ILogger<TransactionalUnitOfWork<TDbContext>> logger, IServiceProvider serviceProvider)
        : base(dbContext, logger)
    {
        ServiceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IUnitOfWork, Task> action) => ExecuteAsync(action, IsolationLevel.ReadCommitted, CancellationToken.None);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IUnitOfWork, Task> action, IsolationLevel isolationLevel) => ExecuteAsync(action, isolationLevel, CancellationToken.None);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IUnitOfWork, Task> action, CancellationToken cancellationToken) => ExecuteAsync(action, IsolationLevel.ReadCommitted, cancellationToken);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IUnitOfWork, Task> action, IsolationLevel isolationLevel, CancellationToken cancellationToken) => ExecuteTransactionAsync(action, isolationLevel, cancellationToken);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IServiceProvider, Task> spAction) => ExecuteTransactionAsync(spAction: spAction, IsolationLevel.ReadCommitted, CancellationToken.None);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IServiceProvider, Task> spAction, IsolationLevel isolationLevel) => ExecuteTransactionAsync(spAction: spAction, isolationLevel, CancellationToken.None);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IServiceProvider, Task> spAction, CancellationToken cancellationToken) => ExecuteTransactionAsync(spAction: spAction, IsolationLevel.ReadCommitted, cancellationToken);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IServiceProvider, Task> spAction, IsolationLevel isolationLevel, CancellationToken cancellationToken) => ExecuteTransactionAsync(spAction: spAction, isolationLevel, cancellationToken);

    /// <inheritdoc />
    public Task ExecuteWithAsync<T>(Func<T, Task> action) => ExecuteTransactionAsync(action, IsolationLevel.ReadCommitted, CancellationToken.None);

    /// <inheritdoc />
    public Task ExecuteWithAsync<T>(Func<T, Task> action, IsolationLevel isolationLevel) => ExecuteTransactionAsync(action, isolationLevel, CancellationToken.None);

    /// <inheritdoc />
    public Task ExecuteWithAsync<T>(Func<T, Task> action, CancellationToken cancellationToken) => ExecuteTransactionAsync(action, IsolationLevel.ReadCommitted, cancellationToken);

    /// <inheritdoc />
    public Task ExecuteWithAsync<T>(Func<T, Task> action, IsolationLevel isolationLevel, CancellationToken cancellationToken) => ExecuteTransactionAsync(action, isolationLevel, cancellationToken);

    private async Task ExecuteTransactionAsync<T>(Func<T, Task> action, IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        IExecutionStrategy strategy = DbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using AsyncServiceScope scope = ServiceProvider.CreateAsyncScope();

            await using TDbContext db = scope.ServiceProvider.GetRequiredService<TDbContext>();
            await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

            try
            {
                await action(scope.ServiceProvider.GetRequiredService<T>());

                await db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Transaction failed while creating an execution strategy");
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    private async Task ExecuteTransactionAsync(Func<IServiceProvider, Task> spAction, IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        IExecutionStrategy strategy = DbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using AsyncServiceScope scope = ServiceProvider.CreateAsyncScope();

            await using TDbContext db = scope.ServiceProvider.GetRequiredService<TDbContext>();
            await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

            try
            {
                await spAction(scope.ServiceProvider);

                await db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Transaction failed while creating an execution strategy");
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }
}