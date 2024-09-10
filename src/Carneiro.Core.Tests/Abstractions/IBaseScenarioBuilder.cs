namespace Carneiro.Core.Tests.Abstractions;

/// <summary>
/// Base scenario builder interface.
/// </summary>
public interface IBaseScenarioBuilder
{
    /// <summary>
    /// Uses the in memory database.
    /// </summary>
    IBaseScenarioBuilder UseInMemoryDatabase<T>() where T : DbContext;

    /// <summary>
    /// Uses the SQL lite.
    /// </summary>
    IBaseScenarioBuilder UseSqlLite<T>() where T : DbContext;

    /// <summary>
    /// Uses the SQL server.
    /// </summary>
    IBaseScenarioBuilder UseSqlServer<T>() where T : DbContext;

    /// <summary>
    /// Uses the SQL lite in memory.
    /// </summary>
    IBaseScenarioBuilder UseSqlLiteInMemory<T>() where T : DbContext;

    /// <summary>
    /// Uses the no data.
    /// </summary>
    IBaseScenarioBuilder UseNoData<T>() where T : DbContext;

    /// <summary>
    /// Starts the HTTP server.
    /// </summary>
    IBaseScenarioBuilder StartHttpServer();

    /// <summary>
    /// Withes the service.
    /// </summary>
    /// <param name="service">The service.</param>
    IBaseScenarioBuilder WithService(Action<IServiceCollection> service);

    /// <summary>
    /// Sets the startup class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    IBaseScenarioBuilder OfStartUp<T>() where T : class;

    /// <summary>
    /// Sets the scenario with environment name.
    /// </summary>
    /// <param name="environment">The environment.</param>
    IBaseScenarioBuilder WithEnvironment(string environment);

    /// <summary>
    /// Adds specific options for <see cref="DatabaseOptions"/>.
    /// </summary>
    /// <param name="action"></param>
    IBaseScenarioBuilder WithDatabaseOptions(Action<DatabaseOptions> action);

    /// <summary>
    /// Action to be performed When scenario the disposes.
    /// </summary>
    /// <param name="action">The action.</param>
    IBaseScenarioBuilder WhenDispose(Action action);

    /// <summary>
    /// Afters the build.
    /// </summary>
    /// <param name="action">The action.</param>
    IBaseScenarioBuilder AfterBuild(Action<IServiceProvider> action);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    IBaseScenarioBuilder WithSetting(string key, string value);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">if set to <c>true</c> [value].</param>
    IBaseScenarioBuilder WithSetting(string key, bool value);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    IBaseScenarioBuilder WithSetting(string key, int value);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    IBaseScenarioBuilder WithSetting(string key, long value);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    IBaseScenarioBuilder WithSetting(string key, decimal value);

    /// <summary>
    /// Adds a new mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    IBaseScenarioBuilder WithMock<T>() where T : class;

    /// <summary>
    /// Adds a new mock with specific <paramref name="action"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    IBaseScenarioBuilder WithMock<T>(Action<Mock<T>> action) where T : class;

    /// <summary>
    /// Adds a new mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="I"></typeparam>
    IBaseScenarioBuilder WithMock<T, I>()
        where T : class
        where I : class, T;

    /// <summary>
    /// Replaces the <see cref="IAntiforgery"/> framework service with a fake one.
    /// </summary>
    IBaseScenarioBuilder WithFakeAntiForgery();

    /// <summary>
    /// Gets the task builder.
    /// </summary>
    /// <value>
    /// The task builder.
    /// </value>
    ITaskBuilder TaskBuilder { get; }

    /// <summary>
    /// Builds this instance.
    /// </summary>
    IBaseScenarioOptions Build();
}