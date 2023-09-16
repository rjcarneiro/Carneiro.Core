using Carneiro.Core.Tests.Scenarios;

namespace Carneiro.Core.Tests.Tasks;

/// <summary>
/// Interface to execute after a scenario is built.
/// </summary>
public interface IAfterBuilderAsyncTask
{
    /// <summary>
    /// Executes a specific task after the <see cref="BaseScenario" /> builds asynchronously.
    /// </summary>
    /// <param name="baseScenario">The base scenario.</param>
    Task ExecuteAfterAsync(BaseScenario baseScenario);
}