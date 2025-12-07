namespace Carneiro.Core.Host;

/// <summary>
/// Periodic scoped background service based in <see cref="PeriodicBackgroundServiceOptions" />.
/// </summary>
public abstract class PeriodicScopedBackgroundService : PeriodicBackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="PeriodicBackgroundService" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    /// <param name="serviceScopeFactory">The service scope factory.</param>
    protected PeriodicScopedBackgroundService(ILogger<PeriodicScopedBackgroundService> logger, IOptions<PeriodicBackgroundServiceOptions> options, IServiceScopeFactory serviceScopeFactory)
        : base(logger, options)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    /// <inheritdoc />
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            await using var asyncServiceScope = _serviceScopeFactory.CreateAsyncScope();
            await RunScopedAsync(asyncServiceScope.ServiceProvider, cancellationToken);
        }
    }

    /// <summary>
    /// Executes the <see cref="PeriodicScopedBackgroundService"/> with a new <see cref="AsyncServiceScope"/>.
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="cancellationToken"></param>
    protected abstract Task RunScopedAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);
}