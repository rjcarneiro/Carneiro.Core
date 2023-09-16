namespace Carneiro.Core.Tests.Tasks;

/// <summary>
/// The base implementation for <see cref="ITaskBuilder"/>.
/// </summary>
/// <seealso cref="ITaskBuilder" />
public class TaskBuilder : ITaskBuilder
{
    private readonly TaskBuilderOptions _tasks;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskBuilder"/> class.
    /// </summary>
    public TaskBuilder()
    {
        _tasks = new TaskBuilderOptions();
    }

    /// <inheritdoc />
    public virtual ITaskBuilder WithAfterBuilderAsyncTask(IAfterBuilderAsyncTask afterBuilderAsyncTask)
    {
        _tasks.AfterBuilderAsyncTasks.Add(afterBuilderAsyncTask);
        return this;
    }

    /// <inheritdoc />
    public virtual ITaskBuilder WithAfterBuilderTask(IAfterBuilderTask afterBuilderTask)
    {
        _tasks.AfterBuilderTasks.Add(afterBuilderTask);
        return this;
    }

    /// <inheritdoc />
    public virtual ITaskBuilder WithBeforeBuilderTask(IBeforeBuilderTask beforeBuilderTask)
    {
        _tasks.BeforeBuilderTasks.Add(beforeBuilderTask);
        return this;
    }

    /// <inheritdoc />
    public virtual ITaskBuilder WithBeforeBuilderAsyncTask(IBeforeBuilderAsyncTask beforeBuilderAsyncTask)
    {
        _tasks.BeforeBuilderAsyncTasks.Add(beforeBuilderAsyncTask);
        return this;
    }

    /// <inheritdoc />
    public virtual ITaskBuilderOptions Build() => _tasks;
}