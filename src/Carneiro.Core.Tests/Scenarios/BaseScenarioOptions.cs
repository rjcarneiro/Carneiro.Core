﻿namespace Carneiro.Core.Tests.Scenarios;

/// <summary>
/// Base scenario options.
/// </summary>
/// <seealso cref="IBaseScenarioOptions" />
public class BaseScenarioOptions : IBaseScenarioOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseScenarioOptions"/> class.
    /// </summary>
    protected BaseScenarioOptions()
    {
    }

    /// <inheritdoc />
    public virtual Type Startup { get; set; } = typeof(TestStartup);

    /// <inheritdoc />
    public virtual List<JsonSettingsItem> JsonSettings { get; } =
    [
        new("appsettings.json", true),
        new("appsettings.LocalTests.json", true)
    ];

    /// <inheritdoc />
    public virtual string Environment { get; set; } = WebHostEnvironmentConstants.Testing;

    /// <inheritdoc />
    public virtual ICollection<Action<IServiceCollection>> Services { get; } = new List<Action<IServiceCollection>>();

    /// <inheritdoc />
    public virtual ICollection<Action<IServiceCollection, IConfiguration>> DatabaseServices { get; } = new List<Action<IServiceCollection, IConfiguration>>();

    /// <inheritdoc />
    public virtual bool HttpServer { get; set; } = false;

    /// <inheritdoc />
    public virtual ScenarioType ScenarioType { get; set; } = ScenarioType.InMemory;

    /// <inheritdoc />
    public virtual ICollection<Action> WhenDispose { get; } = new List<Action>();

    /// <inheritdoc />
    public virtual ICollection<Func<Task>> WhenDisposeAsync { get; } = new List<Func<Task>>();

    /// <inheritdoc />
    public virtual ICollection<Action<IServiceProvider>> AfterBuild { get; } = new List<Action<IServiceProvider>>();

    /// <inheritdoc />
    public virtual ICollection<Func<IServiceProvider, Task>> AfterBuildAsync { get; } = new List<Func<IServiceProvider, Task>>();

    /// <inheritdoc />
    public virtual IDictionary<string, string> Settings { get; } = new Dictionary<string, string>();

    /// <inheritdoc />
    public virtual ITaskBuilderOptions TaskBuilderOptions { get; set; } = new TaskBuilderOptions();

    /// <inheritdoc />
    public virtual IDictionary<Type, Mock> Mocks { get; } = new Dictionary<Type, Mock>();

    /// <inheritdoc />
    public virtual Action<DatabaseOptions> DatabaseOptions { get; set; }
}