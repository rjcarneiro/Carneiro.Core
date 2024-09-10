namespace Carneiro.Core.Tests.Abstractions;

/// <summary>
/// Interface to execute before a scenario is built.
/// </summary>
public interface IBeforeBuilderTask
{
    /// <summary>
    /// Executes a specific task before the <see cref="IBaseScenario" /> builds the infrastructure and creates the entities.
    /// </summary>
    /// <param name="baseScenario">The base scenario.</param>
    void ExecuteBefore(IBaseScenario baseScenario);
}