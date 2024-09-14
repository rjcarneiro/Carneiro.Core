namespace Carneiro.Core.Tests.Abstractions;

/// <summary>
/// Interface to execute after a scenario is built.
/// </summary>
public interface IAfterBuilderTask
{
    /// <summary>
    /// Executes a specific task after the <see cref="IBaseScenario" /> builds.
    /// </summary>
    /// <param name="baseScenario">The base scenario.</param>
    void ExecuteAfter(IBaseScenario baseScenario);
}