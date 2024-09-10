namespace Carneiro.Core.Tests.Abstractions;

/// <summary>
/// Interface to execute before a scenario is built.
/// </summary>
public interface IBeforeBuilderAsyncTask
{
    /// <summary>
    /// Executes a task before the <see cref="IBaseScenario" /> starts building asynchronously.
    /// </summary>
    /// <param name="baseScenario">The base scenario.</param>
    Task ExecuteBeforeAsync(IBaseScenario baseScenario);
}