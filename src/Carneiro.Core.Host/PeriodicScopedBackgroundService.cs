namespace Carneiro.Core.Host;

/// <summary>
/// Periodic scoped background service based in <see cref="PeriodicBackgroundServiceOptions" />.
/// </summary>
public abstract class PeriodicScopedBackgroundService : PeriodicBackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="PeriodicBackgroundService" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    /// <param name="serviceProvider">The service provider.</param>
    protected PeriodicScopedBackgroundService(ILogger<PeriodicScopedBackgroundService> logger, IOptions<PeriodicBackgroundServiceOptions> options, IServiceProvider serviceProvider)
        : base(logger, options)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            await using AsyncServiceScope asyncServiceScope = _serviceProvider.CreateAsyncScope();
            await RunScopedAsync(asyncServiceScope, cancellationToken);
        }
    }

    /// <summary>
    /// Executes the <see cref="PeriodicScopedBackgroundService"/> with a new <see cref="AsyncServiceScope"/>.
    /// </summary>
    /// <param name="asyncServiceScope"></param>
    /// <param name="cancellationToken"></param>
    protected abstract Task RunScopedAsync(AsyncServiceScope asyncServiceScope, CancellationToken cancellationToken);
}