namespace Carneiro.Core.Tests.Tasks;

/// <summary>
/// The default <see cref="ITaskBuilderOptions"/> implementation.
/// </summary>
/// <seealso cref="ITaskBuilderOptions" />
public class TaskBuilderOptions : ITaskBuilderOptions
{
    /// <inheritdoc />
    public virtual ICollection<IAfterBuilderAsyncTask> AfterBuilderAsyncTasks { get; } = new List<IAfterBuilderAsyncTask>();

    /// <inheritdoc />
    public virtual ICollection<IAfterBuilderTask> AfterBuilderTasks { get; } = new List<IAfterBuilderTask>();

    /// <inheritdoc />
    public virtual ICollection<IBeforeBuilderTask> BeforeBuilderTasks { get; } = new List<IBeforeBuilderTask>();

    /// <inheritdoc />
    public virtual ICollection<IBeforeBuilderAsyncTask> BeforeBuilderAsyncTasks { get; } = new List<IBeforeBuilderAsyncTask>();
}