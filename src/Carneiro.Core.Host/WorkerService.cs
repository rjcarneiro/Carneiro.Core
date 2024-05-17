namespace Carneiro.Core.Host;

/// <summary>
/// The main worker service that handles all <see cref="IWorker"/>.
/// </summary>
/// <param name="logger"></param>
/// <param name="serviceProvider"></param>
public class WorkerService(ILogger<WorkerService> logger, IServiceProvider serviceProvider) : OnceOffBackgroundService(logger, serviceProvider)
{
    /// <inheritdoc />
    protected override string TaskName => "HostedService MainWorker";

    /// <inheritdoc />
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
        var workers = ServiceProvider.GetRequiredService<IEnumerable<IWorker>>().ToList();
        var tasks = new List<Task<string>>(workers.Count);

        workers.ForEach(worker =>
        {
            Logger.LogInformation("Adding {WorkerName} into task list", worker.WorkerName);

            tasks.Add(Task.Run(async () =>
            {
                Logger.LogInformation("Starting new async scope for task {WorkerName}", worker.WorkerName);

                await using AsyncServiceScope asyncScope = ServiceProvider.CreateAsyncScope();
                await worker.ExecuteAsync(asyncScope.ServiceProvider, cancellationToken);

                return worker.WorkerName;
            }, cancellationToken));
        });

        while (tasks.Any())
        {
            var finishedTask = await Task.WhenAny(tasks);
            tasks.Remove(finishedTask);
            Logger.LogInformation("{TaskName} task finished with status {TaskStatus}", finishedTask.Result, finishedTask.Status);
        }
    }
}