﻿using Carneiro.Core.Health;

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
    protected PeriodicBackgroundService(ILogger<PeriodicBackgroundService> logger, IOptions<PeriodicBackgroundServiceOptions> options)
        : base(logger)
    {
        Options = options.Value ?? new PeriodicBackgroundServiceOptions();
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var isSuccess = true;

        try
        {
            Logger.LogInformation("Starting periodic service '{TaskName}' version {Version} using options {Options}", TaskName, VersionHelper.GetVersion(), Options);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await RunAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Unable to perform '{TaskName}' on folder '{CurrentDirectory}'", TaskName, Directory.GetCurrentDirectory());
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var seconds = Random.Shared.Next(Options.Min, Options.Max);
                Logger.LogInformation("Waiting {Seconds}s for the next iteration for service '{TaskName}'", seconds, TaskName);
                await Task.Delay(seconds * 1000, cancellationToken);
            }
        }
        catch (Exception e)
        {
            if (e is not OperationCanceledException)
            {
                isSuccess = false;
                Logger.LogCritical(e, "An unknown error happening when running {TaskName}", TaskName);
            }
        }
        finally
        {
            Logger.LogInformation("Finish service '{TaskName}' v{Version}", TaskName, VersionHelper.GetSimplerVersion());
        }

        Environment.Exit(isSuccess ? 0 : 1);
    }
}