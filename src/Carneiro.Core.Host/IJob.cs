namespace Carneiro.Core.Host;

/// <summary>
/// Job interface.
/// </summary>
public interface IJob
{
    /// <summary>
    /// Gets the job name.
    /// </summary>
    string JobName { get; }

    /// <summary>
    /// Executes a simple job.
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="cancellationToken"></param>
    Task DoAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);
}