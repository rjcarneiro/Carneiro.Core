namespace Carneiro.Core.Host;

/// <summary>
/// Default background service that just runs once.
/// </summary>
/// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
public abstract class OnceOffBackgroundService : BaseBackgroundService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OnceOffBackgroundService" /> class.
    /// </summary>
    /// <param name="logger"></param>
    protected OnceOffBackgroundService(ILogger<OnceOffBackgroundService> logger)
        : base(logger)
    {
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var version = VersionHelper.GetSimplerVersion();

        try
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                Logger.LogInformation("Starting service '{TaskName}' v{Version}", TaskName, version);

                await RunAsync(cancellationToken);
            }
        }
        catch (Exception e)
        {
            if (e is not OperationCanceledException)
            {
                Logger.LogError(e, "An unknown error happening when running {TaskName}", TaskName);
            }
        }
        finally
        {
            Logger.LogInformation("Finish service '{TaskName}' v{Version}", TaskName, version);
        }
    }
}