using Carneiro.Core.Tests.Scenarios;

namespace Carneiro.Core.Tests.Tasks;

/// <summary>
/// Interface to execute before a scenario is built.
/// </summary>
public interface IBeforeBuilderAsyncTask
{
    /// <summary>
    /// Executes a task before the <see cref="BaseScenario" /> starts building asynchronously.
    /// </summary>
    /// <param name="baseScenario">The base scenario.</param>
    /// <returns></returns>
    Task ExecuteBeforeAsync(BaseScenario baseScenario);
}