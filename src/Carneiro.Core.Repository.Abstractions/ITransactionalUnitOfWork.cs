using Microsoft.EntityFrameworkCore.Storage;

namespace Carneiro.Core.Repository.Abstractions;

/// <summary>
/// The unit of work transactional interface.
/// </summary>
public interface ITransactionalUnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <see cref="IsolationLevel.ReadCommitted"/>.
    /// </summary>
    /// <param name="action"></param>
    Task ExecuteAsync(Func<IUnitOfWork, Task> action);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <paramref name="isolationLevel"/>.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="isolationLevel"></param>
    Task ExecuteAsync(Func<IUnitOfWork, Task> action, IsolationLevel isolationLevel);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <see cref="IsolationLevel.ReadCommitted"/>.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    Task ExecuteAsync(Func<IUnitOfWork, Task> action, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <paramref name="isolationLevel"/>.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="isolationLevel"></param>
    /// <param name="cancellationToken"></param>
    Task ExecuteAsync(Func<IUnitOfWork, Task> action, IsolationLevel isolationLevel, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <see cref="IsolationLevel.ReadCommitted"/>.
    /// </summary>
    /// <param name="spAction"></param>
    Task ExecuteAsync(Func<IServiceProvider, Task> spAction);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <paramref name="isolationLevel"/>.
    /// </summary>
    /// <param name="spAction"></param>
    /// <param name="isolationLevel"></param>
    Task ExecuteAsync(Func<IServiceProvider, Task> spAction, IsolationLevel isolationLevel);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <see cref="IsolationLevel.ReadCommitted"/>.
    /// </summary>
    /// <param name="spAction"></param>
    /// <param name="cancellationToken"></param>
    Task ExecuteAsync(Func<IServiceProvider, Task> spAction, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <paramref name="isolationLevel"/>.
    /// </summary>
    /// <param name="spAction"></param>
    /// <param name="isolationLevel"></param>
    /// <param name="cancellationToken"></param>
    Task ExecuteAsync(Func<IServiceProvider, Task> spAction, IsolationLevel isolationLevel, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <see cref="IsolationLevel.ReadCommitted"/>. It will resolve a <typeparamref name="T"/> from a new scope that allow to invoke methods from existing services.
    /// </summary>
    /// <param name="action"></param>
    /// <typeparam name="T">The class or interface which should be resolved to make the transaction commit.</typeparam>
    Task ExecuteWithAsync<T>(Func<T, Task> action);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <paramref name="isolationLevel"/>. It will resolve a <typeparamref name="T"/> from a new scope that allow to invoke methods from existing services.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="isolationLevel"></param>
    /// <typeparam name="T">The class or interface which should be resolved to make the transaction commit.</typeparam>
    Task ExecuteWithAsync<T>(Func<T, Task> action, IsolationLevel isolationLevel);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <see cref="IsolationLevel.ReadCommitted"/>. It will resolve a <typeparamref name="T"/> from a new scope that allow to invoke methods from existing services.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The class or interface which should be resolved to make the transaction commit.</typeparam>
    Task ExecuteWithAsync<T>(Func<T, Task> action, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new <see cref="IExecutionStrategy"/> and uses a <see cref="IDbContextTransaction"/> with <paramref name="isolationLevel"/>. It will resolve a <typeparamref name="T"/> from a new scope that allow to invoke methods from existing services.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="isolationLevel"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The class or interface which should be resolved to make the transaction commit.</typeparam>
    Task ExecuteWithAsync<T>(Func<T, Task> action, IsolationLevel isolationLevel, CancellationToken cancellationToken);
}