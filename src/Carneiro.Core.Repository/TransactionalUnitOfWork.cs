namespace Carneiro.Core.Repository;

/// <summary>
/// The unit of work transactional implementation.
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public class TransactionalUnitOfWork<TDbContext> : ITransactionalUnitOfWork<TDbContext> where TDbContext : DbContext
{
    /// <summary>
    /// Gets the <see cref="IServiceProvider"/>.
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    protected ILogger<TransactionalUnitOfWork<TDbContext>> Logger { get; }

    /// <summary>
    /// Gets the <typeparamref name="TDbContext"/>.
    /// </summary>
    protected TDbContext DbContext { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionalUnitOfWork{TDbContext}"/> class.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="serviceProvider"></param>
    /// <param name="dbContext"></param>
    public TransactionalUnitOfWork(ILogger<TransactionalUnitOfWork<TDbContext>> logger, IServiceProvider serviceProvider, TDbContext dbContext)
    {
        Logger = logger;
        ServiceProvider = serviceProvider;
        DbContext = dbContext;
    }

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IUnitOfWork<TDbContext>, Task> action) => ExecuteAsync(action, IsolationLevel.ReadCommitted, CancellationToken.None);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IUnitOfWork<TDbContext>, Task> action, IsolationLevel isolationLevel) => ExecuteAsync(action, isolationLevel, CancellationToken.None);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IUnitOfWork<TDbContext>, Task> action, CancellationToken cancellationToken) => ExecuteAsync(action, IsolationLevel.ReadCommitted, cancellationToken);

    /// <inheritdoc />
    public Task ExecuteAsync(Func<IUnitOfWork<TDbContext>, Task> action, IsolationLevel isolationLevel, CancellationToken cancellationToken) => ExecuteTransactionAsync(action, isolationLevel, cancellationToken);

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

    /// <inheritdoc />
    public Task<T> ExecuteWithResultAsync<T>(Func<IUnitOfWork<TDbContext>, Task<T>> action) => ExecuteTransactionWithResultAsync(action, IsolationLevel.ReadCommitted, CancellationToken.None);

    /// <inheritdoc />
    public Task<T> ExecuteWithResultAsync<T>(Func<IUnitOfWork<TDbContext>, Task<T>> action, IsolationLevel isolationLevel) => ExecuteTransactionWithResultAsync(action, isolationLevel, CancellationToken.None);

    /// <inheritdoc />
    public Task<T> ExecuteWithResultAsync<T>(Func<IUnitOfWork<TDbContext>, Task<T>> action, CancellationToken cancellationToken) => ExecuteTransactionWithResultAsync(action, IsolationLevel.ReadCommitted, cancellationToken);

    /// <inheritdoc />
    public Task<T> ExecuteWithResultAsync<T>(Func<IUnitOfWork<TDbContext>, Task<T>> action, IsolationLevel isolationLevel, CancellationToken cancellationToken) => ExecuteTransactionWithResultAsync(action, isolationLevel, cancellationToken);

    /// <inheritdoc />
    public Task<T> ExecuteWithResultAsync<T>(Func<IServiceProvider, Task<T>> action) => ExecuteTransactionWithResultAsync(action, IsolationLevel.ReadCommitted, CancellationToken.None);

    /// <inheritdoc />
    public Task<T> ExecuteWithResultAsync<T>(Func<IServiceProvider, Task<T>> action, IsolationLevel isolationLevel) => ExecuteTransactionWithResultAsync(action, isolationLevel, CancellationToken.None);

    /// <inheritdoc />
    public Task<T> ExecuteWithResultAsync<T>(Func<IServiceProvider, Task<T>> action, CancellationToken cancellationToken) => ExecuteTransactionWithResultAsync<T>(action, IsolationLevel.ReadCommitted, cancellationToken);

    /// <inheritdoc />
    public Task<T> ExecuteWithResultAsync<T>(Func<IServiceProvider, Task<T>> action, IsolationLevel isolationLevel, CancellationToken cancellationToken) =>
        ExecuteTransactionWithResultAsync(action, isolationLevel, cancellationToken);

    /// <inheritdoc />
    public Task<Y> ExecuteWithResultAsync<T, Y>(Func<T, Task<Y>> action) => ExecuteTransactionWithResultAsync(action, IsolationLevel.ReadCommitted, CancellationToken.None);

    /// <inheritdoc />
    public Task<Y> ExecuteWithResultAsync<T, Y>(Func<T, Task<Y>> action, IsolationLevel isolationLevel) => ExecuteTransactionWithResultAsync(action, isolationLevel, CancellationToken.None);

    /// <inheritdoc />
    public Task<Y> ExecuteWithResultAsync<T, Y>(Func<T, Task<Y>> action, CancellationToken cancellationToken) => ExecuteTransactionWithResultAsync(action, IsolationLevel.ReadCommitted, cancellationToken);

    /// <inheritdoc />
    public Task<Y> ExecuteWithResultAsync<T, Y>(Func<T, Task<Y>> action, IsolationLevel isolationLevel, CancellationToken cancellationToken) => ExecuteTransactionWithResultAsync(action, isolationLevel, cancellationToken);

    private Task ExecuteTransactionAsync<T>(Func<T, Task> action, IsolationLevel isolationLevel, CancellationToken cancellationToken) =>
        ExecuteTransactionCoreAsync(action, isolationLevel, isServiceProvider: false, cancellationToken);

    private Task ExecuteTransactionAsync(Func<IServiceProvider, Task> spAction, IsolationLevel isolationLevel, CancellationToken cancellationToken) =>
        ExecuteTransactionCoreAsync(spAction, isolationLevel, isServiceProvider: true, cancellationToken);

    private async Task ExecuteTransactionCoreAsync<T>(Func<T, Task> action, IsolationLevel isolationLevel, bool isServiceProvider, CancellationToken cancellationToken)
    {
        IExecutionStrategy strategy = DbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using AsyncServiceScope newScope = ServiceProvider.CreateAsyncScope();

            await using TDbContext db = newScope.ServiceProvider.GetRequiredService<TDbContext>();
            await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(isolationLevel, cancellationToken);


            try
            {
                if (isServiceProvider)
                {
                    await action((T)newScope.ServiceProvider);
                }
                else
                {
                    await action(newScope.ServiceProvider.GetRequiredService<T>());
                }

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

    private Task<Y> ExecuteTransactionWithResultAsync<T, Y>(Func<T, Task<Y>> spAction, IsolationLevel isolationLevel, CancellationToken cancellationToken) =>
        ExecuteTransactionWithResultCoreAsync(spAction, isolationLevel, isServiceProvider: false, cancellationToken);

    private Task<Y> ExecuteTransactionWithResultAsync<Y>(Func<IServiceProvider, Task<Y>> spAction, IsolationLevel isolationLevel, CancellationToken cancellationToken) =>
        ExecuteTransactionWithResultCoreAsync(spAction, isolationLevel, isServiceProvider: true, cancellationToken);

    private async Task<Y> ExecuteTransactionWithResultCoreAsync<T, Y>(Func<T, Task<Y>> action, IsolationLevel isolationLevel, bool isServiceProvider, CancellationToken cancellationToken)
    {
        IExecutionStrategy strategy = DbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using AsyncServiceScope newScope = ServiceProvider.CreateAsyncScope();

            await using TDbContext db = newScope.ServiceProvider.GetRequiredService<TDbContext>();
            await using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

            try
            {
                Y result;

                if (isServiceProvider)
                {
                    result = await action((T)newScope.ServiceProvider);
                }
                else
                {
                    result = await action(newScope.ServiceProvider.GetRequiredService<T>());
                }

                await db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return result;
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