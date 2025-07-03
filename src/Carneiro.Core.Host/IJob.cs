namespace Carneiro.Core.Host;

/// <summary>
/// Job interface.
/// </summary>
public interface IJob
{
    /// <summary>
    /// Gets the worker name.
    /// </summary>
    string JobName { get; }

    /// <summary>
    /// Executes the simple work.
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task KickOffAsync(CancellationToken cancellationToken);
}