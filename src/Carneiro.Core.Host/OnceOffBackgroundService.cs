using Carneiro.Core.Health;

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
        var isSuccess = true;
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
                isSuccess = false;
                Logger.LogError(e, "An unknown error happening when running {TaskName}", TaskName);
            }
        }
        finally
        {
            Logger.LogInformation("Finish service '{TaskName}' v{Version}", TaskName, version);
        }

        Environment.Exit(isSuccess ? 0 : 1);
    }
}