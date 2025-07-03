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
    /// <param name="cancellationToken"></param>
    Task KickOffAsync(CancellationToken cancellationToken);
}