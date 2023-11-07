namespace Carneiro.Core.Tests.Abstractions;

/// <summary>
/// Default options for scenario before and after builder tasks.
/// </summary>
public interface ITaskBuilderOptions
{
    /// <summary>
    /// Gets the after builder tasks.
    /// </summary>
    /// <value>
    /// The after builder tasks.
    /// </value>
    ICollection<IAfterBuilderAsyncTask> AfterBuilderAsyncTasks { get; }

    /// <summary>
    /// Gets the after builder tasks.
    /// </summary>
    /// <value>
    /// The after builder tasks.
    /// </value>
    ICollection<IAfterBuilderTask> AfterBuilderTasks { get; }

    /// <summary>
    /// Gets the before builder tasks.
    /// </summary>
    /// <value>
    /// The before builder tasks.
    /// </value>
    ICollection<IBeforeBuilderTask> BeforeBuilderTasks { get; }

    /// <summary>
    /// Gets the before builder asynchronous tasks.
    /// </summary>
    /// <value>
    /// The before builder asynchronous tasks.
    /// </value>
    ICollection<IBeforeBuilderAsyncTask> BeforeBuilderAsyncTasks { get; }
}