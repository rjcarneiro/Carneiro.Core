namespace Carneiro.Core.Host;

/// <summary>
/// The default class for background services.
/// </summary>
public abstract class BaseBackgroundService : BackgroundService
{
    /// <summary>
    /// Gets the <see cref="ILogger"/>.
    /// </summary>
    protected virtual ILogger<BaseBackgroundService> Logger { get; }

    /// <summary>
    /// Gets the <see cref="IHostApplicationLifetime"/>.
    /// </summary>
    protected IHostApplicationLifetime ApplicationLifetime { get; }

    /// <summary>
    /// Gets the name of the task.
    /// </summary>
    protected abstract string TaskName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OnceOffBackgroundService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="hostApplicationLifetime"></param>
    protected BaseBackgroundService(ILogger<BaseBackgroundService> logger, IHostApplicationLifetime hostApplicationLifetime)
    {
        Logger = logger;
        ApplicationLifetime = hostApplicationLifetime;
    }

    /// <summary>
    /// Runs the task asynchronously.
    /// </summary>
    /// <param name="token">The token.</param>
    protected abstract Task RunAsync(CancellationToken token);
}