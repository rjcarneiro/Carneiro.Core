namespace Carneiro.Core.Host;

/// <summary>
/// The main worker service that handles all <see cref="IJob"/>.
/// </summary>
/// <param name="logger"></param>
/// <param name="serviceProvider"></param>
public class JobOnceOffBackgroundService(ILogger<JobOnceOffBackgroundService> logger, IServiceProvider serviceProvider) : OnceOffBackgroundService(logger, serviceProvider)
{
    /// <inheritdoc />
    protected override string TaskName => "Main Job Service";

    /// <inheritdoc />
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
        var executedInitializers = new List<Type>();
        var tasks = new List<Task<string>>();

        while (true)
        {
            var scope = ServiceProvider.CreateAsyncScope();

            var worker = scope.ServiceProvider.GetRequiredService<IEnumerable<IJob>>()
                .FirstOrDefault(i => !executedInitializers.Contains(i.GetType()));

            if (worker is null)
            {
                break;
            }

            executedInitializers.Add(worker.GetType());

            tasks.Add(Task.Run(async () =>
            {
                Logger.LogInformation("Starting new job '{JobName}'", worker.JobName);

                try
                {
                    await worker.DoAsync(cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    // do nothing
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Error while running job '{JobName}'", worker.JobName);
                }
                finally
                {
                    await scope.DisposeAsync();
                }

                return worker.JobName;
            }, cancellationToken));
        }

        while (tasks.Count != 0)
        {
            var finishedTask = await Task.WhenAny(tasks);
            tasks.Remove(finishedTask);
            Logger.LogInformation("Job '{JobName}' finished with status {Status}", finishedTask.Result, finishedTask.Status);
        }
    }
}