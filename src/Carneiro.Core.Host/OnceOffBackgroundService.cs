namespace Carneiro.Core.Host;

/// <summary>
/// Default background service that just runs once.
/// </summary>
/// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
public abstract class OnceOffBackgroundService : BaseBackgroundService
{
    /// <summary>
    /// Gets the <see cref="IServiceProvider"/>.
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OnceOffBackgroundService" /> class.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="serviceProvider"></param>
    protected OnceOffBackgroundService(ILogger<OnceOffBackgroundService> logger, IServiceProvider serviceProvider)
        : base(logger)
    {
        ServiceProvider = serviceProvider;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var version = VersionHelper.GetSimplerVersion();
        var stopwatch = Stopwatch.StartNew();

        try
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                Logger.LogInformation("Starting service '{TaskName}' v{Version}", TaskName, version);

                await RunAsync(cancellationToken);
            }
        }
        catch (TaskCanceledException)
        {
            // do nothing
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An unknown error happening when running {TaskName}", TaskName);
        }
        finally
        {
            stopwatch.Stop();
            Logger.LogInformation("Finish service '{TaskName}' v{Version}. Elapsed time: {StopwatchElapsed}", TaskName, version, stopwatch.Elapsed);

            ServiceProvider.GetRequiredService<IHostApplicationLifetime>().StopApplication();
        }
    }
}