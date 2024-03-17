namespace Carneiro.Core.Host;

/// <summary>
/// The base <see cref="IWorker"/> class implementation.
/// </summary>
public abstract class BaseWorker : IWorker
{
    /// <summary>
    /// Gets the <see cref="ILogger"/>.
    /// </summary>
    protected ILogger<BaseWorker> Logger { get; }

    /// <summary>
    /// Gets the name of the task.
    /// </summary>
    public abstract string WorkerName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseWorker"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    protected BaseWorker(ILogger<BaseWorker> logger)
    {
        Logger = logger;
    }

    /// <inheritdoc />
    public async Task ExecuteAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        try
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                await RunAsync(serviceProvider, cancellationToken);
            }
        }
        catch (Exception e)
        {
            if (e is not OperationCanceledException)
            {
                Logger.LogError(e, "An unknown error happening when running {WorkerName}", WorkerName);
            }
        }
    }

    /// <summary>
    /// Runs the worker.
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="cancellationToken"></param>
    protected abstract Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);
}