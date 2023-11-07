namespace Carneiro.Core.Tests.Abstractions;

/// <summary>
/// Interface to execute after a scenario is built.
/// </summary>
public interface IAfterBuilderAsyncTask
{
    /// <summary>
    /// Executes a specific task after the <see cref="IBaseScenario" /> builds asynchronously.
    /// </summary>
    /// <param name="baseScenario">The base scenario.</param>
    Task ExecuteAfterAsync(IBaseScenario baseScenario);
}