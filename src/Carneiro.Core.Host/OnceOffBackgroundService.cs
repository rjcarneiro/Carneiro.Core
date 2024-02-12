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

        try
        {
            Logger.LogInformation("Starting service '{TaskName}' v{Version}", TaskName, VersionHelper.GetSimplerVersion());

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await RunAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // When the stopping token is canceled, for example, a call made from services.msc,
            // we shouldn't exit with a non-zero exit code. In other words, this is expected...
        }
        catch (Exception e)
        {
            isSuccess = false;
            Logger.LogError(e, "An unknown error happening when running {TaskName}", TaskName);
        }
        finally
        {
            Logger.LogInformation("Finish service '{TaskName}' v{Version}", TaskName, VersionHelper.GetSimplerVersion());
        }

        Environment.Exit(isSuccess ? 0 : 1);
    }
}