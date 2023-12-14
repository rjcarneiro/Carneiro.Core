using Carneiro.Core.Repository;
using Microsoft.AspNetCore.Antiforgery;
using Moq;

namespace Carneiro.Core.Tests.Abstractions;

/// <summary>
/// Base scenario builder interface.
/// </summary>
public interface IBaseScenarioBuilder
{
    /// <summary>
    /// Uses the in memory database.
    /// </summary>
    /// <returns></returns>
    IBaseScenarioBuilder UseInMemoryDatabase<T>() where T : RzDbContext;

    /// <summary>
    /// Uses the SQL lite.
    /// </summary>
    /// <returns></returns>
    IBaseScenarioBuilder UseSqlLite<T>() where T : RzDbContext;

    /// <summary>
    /// Uses the SQL server.
    /// </summary>
    /// <returns></returns>
    IBaseScenarioBuilder UseSqlServer<T>() where T : RzDbContext;

    /// <summary>
    /// Uses the SQL lite in memory.
    /// </summary>
    /// <returns></returns>
    IBaseScenarioBuilder UseSqlLiteInMemory<T>() where T : RzDbContext;

    /// <summary>
    /// Uses the no data.
    /// </summary>
    /// <returns></returns>
    IBaseScenarioBuilder UseNoData<T>() where T : RzDbContext;

    /// <summary>
    /// Starts the HTTP server.
    /// </summary>
    /// <returns></returns>
    IBaseScenarioBuilder StartHttpServer();

    /// <summary>
    /// Withes the service.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <returns></returns>
    IBaseScenarioBuilder WithService(Action<IServiceCollection> service);

    /// <summary>
    /// Sets the startup class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IBaseScenarioBuilder OfStartUp<T>() where T : class;

    /// <summary>
    /// Sets the scenario with environment name.
    /// </summary>
    /// <param name="environment">The environment.</param>
    /// <returns></returns>
    IBaseScenarioBuilder WithEnvironment(string environment);

    /// <summary>
    /// Adds specific options for <see cref="DatabaseOptions"/>.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    IBaseScenarioBuilder WithDatabaseOptions(Action<DatabaseOptions> action);

    /// <summary>
    /// Action to be performed When scenario the disposes.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    IBaseScenarioBuilder WhenDispose(Action action);

    /// <summary>
    /// Afters the build.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    IBaseScenarioBuilder AfterBuild(Action<IServiceProvider> action);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    IBaseScenarioBuilder WithSetting(string key, string value);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">if set to <c>true</c> [value].</param>
    /// <returns></returns>
    IBaseScenarioBuilder WithSetting(string key, bool value);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    IBaseScenarioBuilder WithSetting(string key, int value);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    IBaseScenarioBuilder WithSetting(string key, long value);

    /// <summary>
    /// Adds a new setting.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    IBaseScenarioBuilder WithSetting(string key, decimal value);

    /// <summary>
    /// Adds a new mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IBaseScenarioBuilder WithMock<T>() where T : class;

    /// <summary>
    /// Adds a new mock with specific <paramref name="action"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    /// <returns></returns>
    IBaseScenarioBuilder WithMock<T>(Action<Mock<T>> action) where T : class;

    /// <summary>
    /// Adds a new mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="I"></typeparam>
    /// <returns></returns>
    IBaseScenarioBuilder WithMock<T, I>()
        where T : class
        where I : class, T;

    /// <summary>
    /// Replaces the <see cref="IAntiforgery"/> framework service with a fake one.
    /// </summary>
    /// <returns></returns>
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
    /// <returns></returns>
    IBaseScenarioOptions Build();
}