using Carneiro.Core.Repository.Options;
using Carneiro.Core.Tests.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Carneiro.Core.Tests.Scenarios;

/// <summary>
/// Base scenario options interface.
/// </summary>
public interface IBaseScenarioOptions
{
    /// <summary>
    /// Gets or sets the startup.
    /// </summary>
    /// <value>
    /// The startup.
    /// </value>
    Type Startup { get; set; }

    /// <summary>
    /// Gets the environment. Default is <c>Testing</c>.
    /// </summary>
    /// <value>
    /// The environment.
    /// </value>
    string Environment { get; set; }

    /// <summary>
    /// Gets the json settings.
    /// </summary>
    /// <value>
    /// The json settings.
    /// </value>
    /// <remarks>The key is the json file name and the value is either optional or not.</remarks>
    Dictionary<string, bool> JsonSettings { get; }

    /// <summary>
    /// Gets the services.
    /// </summary>
    /// <value>
    /// The services.
    /// </value>
    ICollection<Action<IServiceCollection>> Services { get; }

    /// <summary>
    /// Gets the database services.
    /// </summary>
    /// <value>
    /// The database services.
    /// </value>
    ICollection<Action<IServiceCollection, IConfiguration>> DatabaseServices { get; }

    /// <summary>
    /// Gets or sets a value indicating whether [HTTP server].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [HTTP server]; otherwise, <c>false</c>.
    /// </value>
    bool HttpServer { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    ScenarioType ScenarioType { get; set; }

    /// <summary>
    /// Gets the list of actions to perform when dispose.
    /// </summary>
    /// <value>
    /// The when dispose.
    /// </value>
    ICollection<Action> WhenDispose { get; }

    /// <summary>
    /// Gets the list of actions to perform when dispose asynchronously.
    /// </summary>
    ICollection<Func<Task>> WhenDisposeAsync { get; }

    /// <summary>
    /// Gets or sets the list of actions to perform after the scenario builds.
    /// </summary>
    /// <value>
    /// The after build.
    /// </value>
    ICollection<Action<IServiceProvider>> AfterBuild { get; }

    /// <summary>
    /// Gets or sets the list of actions to perform after the scenario is build asynchronously.
    /// </summary>
    ICollection<Func<IServiceProvider, Task>> AfterBuildAsync { get; }

    /// <summary>
    /// Gets the settings.
    /// </summary>
    /// <value>
    /// The settings.
    /// </value>
    IDictionary<string, string> Settings { get; }

    /// <summary>
    /// Gets or sets the task builder options.
    /// </summary>
    /// <value>
    /// The task builder options.
    /// </value>
    ITaskBuilderOptions TaskBuilderOptions { get; set; }

    /// <summary>
    /// Gets or sets the mocks.
    /// </summary>
    IDictionary<Type, Mock> Mocks { get; }

    /// <summary>
    /// Gets or sets the database options.
    /// </summary>
    Action<DatabaseOptions> DatabaseOptions { get; set; }
}