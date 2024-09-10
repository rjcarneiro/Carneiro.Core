namespace Carneiro.Core.Tests.Abstractions;

/// <summary>
/// Interface that allow builder tasks to be added to the.
/// </summary>
public interface ITaskBuilder
{
    /// <summary>
    /// Adds a new <see cref="IAfterBuilderAsyncTask"/> to be run after the scenario builder is complete.
    /// </summary>
    /// <param name="afterBuilderAsyncTask">The after builder task.</param>
    ITaskBuilder WithAfterBuilderAsyncTask(IAfterBuilderAsyncTask afterBuilderAsyncTask);

    /// <summary>
    /// Adds a new <see cref="IAfterBuilderTask"/> to be run after the scenario builder is complete.
    /// </summary>
    /// <param name="afterBuilderTask">The after builder task.</param>
    ITaskBuilder WithAfterBuilderTask(IAfterBuilderTask afterBuilderTask);

    /// <summary>
    /// Adds a new <see cref="IBeforeBuilderTask"/> to be run before the scenario builder starts.
    /// </summary>
    /// <param name="beforeBuilderTask">The before builder task.</param>
    ITaskBuilder WithBeforeBuilderTask(IBeforeBuilderTask beforeBuilderTask);

    /// <summary>
    /// Adds a new <see cref="IBeforeBuilderAsyncTask"/> to be run before the scenario builder starts.
    /// </summary>
    /// <param name="beforeBuilderAsyncTask">The before builder asynchronous task.</param>
    ITaskBuilder WithBeforeBuilderAsyncTask(IBeforeBuilderAsyncTask beforeBuilderAsyncTask);

    /// <summary>
    /// Builds this instance.
    /// </summary>
    ITaskBuilderOptions Build();
}