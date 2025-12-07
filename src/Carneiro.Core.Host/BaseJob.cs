namespace Carneiro.Core.Host;

/// <summary>
/// The base <see cref="IJob"/> class implementation.
/// </summary>
public abstract class BaseJob : IJob
{
    /// <summary>
    /// Gets the <see cref="ILogger"/>.
    /// </summary>
    protected ILogger<BaseJob> Logger { get; }

    /// <summary>
    /// Gets the name of the task.
    /// </summary>
    public abstract string JobName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseJob"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    protected BaseJob(ILogger<BaseJob> logger)
    {
        Logger = logger;
    }

    /// <inheritdoc />
    public async Task DoAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        try
        {
            await DoTaskAsync(serviceProvider, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // do nothing
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An unknown error happening when running {JobName}", JobName);
        }
    }

    /// <summary>
    /// Runs the worker.
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="cancellationToken"></param>
    protected abstract Task DoTaskAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);
}
