namespace Carneiro.Core.Host;

/// <summary>
/// Worker interface.
/// </summary>
public interface IWorker
{
    /// <summary>
    /// Gets the worker name.
    /// </summary>
    string WorkerName { get; }

    /// <summary>
    /// Executes the simple work.
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="cancellationToken"></param>
    Task ExecuteAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);
}