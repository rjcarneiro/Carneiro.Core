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
    public async Task KickOffAsync(CancellationToken cancellationToken)
    {
        try
        {
            await DoAsync(cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // do nothing
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Something went wrong with job '{JobName}'", JobName);
        }
    }

    /// <summary>
    /// Starts the job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    protected abstract Task DoAsync(CancellationToken cancellationToken);
}