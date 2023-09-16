using Carneiro.Core.Health;

namespace Carneiro.Core.Host;

/// <summary>
/// Periodic Background Service based in <see cref="PeriodicBackgroundServiceOptions" />.
/// </summary>
public abstract class PeriodicBackgroundService : BaseBackgroundService
{
    /// <summary>
    /// Gets the options.
    /// </summary>
    protected virtual PeriodicBackgroundServiceOptions Options { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PeriodicBackgroundService" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    /// <param name="hostApplicationLifetime"></param>
    protected PeriodicBackgroundService(ILogger<PeriodicBackgroundService> logger, IOptions<PeriodicBackgroundServiceOptions> options,
        IHostApplicationLifetime hostApplicationLifetime)
        : base(logger, hostApplicationLifetime)
    {
        Options = options.Value ?? new PeriodicBackgroundServiceOptions();
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Starting periodic service \'{TaskName}\' v{Version} on folder \'{CurrentDirectory}\' using Options {Options}", TaskName, VersionHelper.GetSimplerVersion(),
            Directory.GetCurrentDirectory(), Options);

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Logger.LogInformation("Looping again into service \'{TaskName}\'", TaskName);

                try
                {
                    await RunAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Unable to perform \'{TaskName}\' on folder \'{CurrentDirectory}\'", TaskName, Directory.GetCurrentDirectory());
                }

                var seconds = Random.Shared.Next(Options.Min, Options.Max);

                Logger.LogInformation("Process finished! Waiting {Seconds} seconds for the next iteration for service \'{TaskName}\'", seconds, TaskName);

                if (cancellationToken.IsCancellationRequested)
                    break;
                
                await Task.Delay(seconds * 1000, cancellationToken);
            }
        }
        finally
        {
            ApplicationLifetime.StopApplication();
        }
    }
}