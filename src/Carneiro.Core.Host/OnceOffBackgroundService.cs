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
    /// <param name="hostApplicationLifetime"></param>
    protected OnceOffBackgroundService(ILogger<OnceOffBackgroundService> logger, IHostApplicationLifetime hostApplicationLifetime) 
        : base(logger, hostApplicationLifetime)
    {
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return;

        Logger.LogInformation("Starting service \'{TaskName}\' v{Version} on folder \'{CurrentDirectory}\'", TaskName, VersionHelper.GetSimplerVersion(), Directory.GetCurrentDirectory());

        try
        {
            await RunAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Unable to perform \'{TaskName}\' on folder \'{CurrentDirectory}\'", TaskName, Directory.GetCurrentDirectory());
        }
        finally
        {
            ApplicationLifetime.StopApplication();
        }
    }
}