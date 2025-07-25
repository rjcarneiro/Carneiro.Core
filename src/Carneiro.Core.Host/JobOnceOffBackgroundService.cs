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
        var workers = ServiceProvider.GetRequiredService<IEnumerable<IJob>>().ToList();
        var tasks = new List<Task<string>>(workers.Count);

        workers.ForEach(worker =>
        {
            Logger.LogInformation("Adding {JobName} into task list", worker.JobName);

            tasks.Add(Task.Run(async () =>
            {
                Logger.LogInformation("Starting new async scope for task {JobName}", worker.JobName);

                await using var asyncScope = ServiceProvider.CreateAsyncScope();
                await worker.DoAsync(asyncScope.ServiceProvider, cancellationToken);

                return worker.JobName;
            }, cancellationToken));
        });

        while (tasks.Count != 0)
        {
            var finishedTask = await Task.WhenAny(tasks);
            tasks.Remove(finishedTask);
            Logger.LogInformation("{TaskName} task finished with status {TaskStatus}", finishedTask.Result, finishedTask.Status);
        }
    }
}