using System.Text;
using Carneiro.Core.Repository.Abstractions;
using Carneiro.Core.Tests.Builders.EntityBuilders;
using Carneiro.Core.Tests.Core;
using Carneiro.Core.Tests.Options;
using Carneiro.Core.Utils.Abstractions;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Carneiro.Core.Tests.Scenarios;

/// <summary>
/// Base class for all scenarios.
/// </summary>
public abstract class BaseScenario : BaseTest, IBaseScenario
{
    private readonly IHostBuilder _webHostBuilder;

    private IHost _webHost;

    /// <summary>
    /// The test server
    /// </summary>
    protected TestServer TestServer { get; private set; }

    /// <summary>
    /// Gets the scenario options.
    /// </summary>
    /// <value>
    /// The scenario options.
    /// </value>
    protected virtual IBaseScenarioOptions ScenarioOptions { get; }

    /// <summary>
    /// Gets the service provider.
    /// </summary>
    /// <value>
    /// The service provider.
    /// </value>
    protected virtual IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// Gets or sets the URL helper.
    /// </summary>
    /// <value>
    /// The URL helper.
    /// </value>
    public virtual Mock<IUrlHelper> Url { get; } = new(MockBehavior.Strict);

    /// <summary>
    /// Gets the ajax request handler.
    /// </summary>
    /// <value>
    /// The ajax request handler.
    /// </value>
    public virtual AjaxRequestHandler AjaxRequestHandler { get; private set; }

    /// <summary>
    /// Gets the file utility.
    /// </summary>
    public virtual Mock<IFileUtil> FileUtil { get; } = new();

    /// <summary>
    /// A list of mocks.
    /// </summary>
    protected virtual IDictionary<Type, Mock> Mocks => ScenarioOptions.Mocks;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseScenario"/> class.
    /// </summary>
    /// <param name="scenarioOptions">The scenario options.</param>
    protected BaseScenario(IBaseScenarioOptions scenarioOptions)
    {
        ScenarioOptions = scenarioOptions;
        _webHostBuilder = new HostBuilder();
    }

    /// <summary>
    /// Gets the required service.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public virtual T GetRequiredService<T>() => ServiceProvider.GetRequiredService<T>();

    /// <summary>
    /// Gets the service.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public virtual T GetService<T>() => ServiceProvider.GetService<T>();

    /// <summary>
    /// Gets the options.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public virtual T GetOptions<T>() where T : class, new() => ServiceProvider.GetService<IOptions<T>>()?.Value;

    /// <summary>
    /// Executes some scoped action asynchronously.
    /// </summary>
    /// <param name="action">The action.</param>
    public virtual async Task ExecuteScopedAsync(Func<IServiceProvider, Task> action)
    {
        using IServiceScope scope = ServiceProvider.CreateScope();
        await action(scope.ServiceProvider);
    }

    /// <summary>
    /// Executes the scoped asynchronously.
    /// </summary>
    /// <param name="action">The action.</param>
    public virtual async Task ExecuteScopedAsync(Func<IServiceProvider, IUnitOfWork, Task> action)
    {
        using IServiceScope scope = ServiceProvider.CreateScope();
        await action(scope.ServiceProvider, scope.ServiceProvider.GetRequiredService<IUnitOfWork>());
    }

    /// <summary>
    /// Executes the scoped asynchronously.
    /// </summary>
    /// <param name="action">The action.</param>
    public virtual async Task ExecuteScopedAsync(Func<IUnitOfWork, Task> action)
    {
        using IServiceScope scope = ServiceProvider.CreateScope();
        await action(scope.ServiceProvider.GetRequiredService<IUnitOfWork>());
    }

    /// <summary>
    /// Executes some scoped action with a <typeparamref name="TReturn"/> return value asynchronously.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public virtual Task<TReturn> ExecuteScopedAsync<TReturn>(Func<IServiceProvider, Task<TReturn>> action)
    {
        using IServiceScope scope = ServiceProvider.CreateScope();
        return action(scope.ServiceProvider);
    }

    /// <summary>
    /// Gets the service provider.
    /// </summary>
    /// <returns></returns>
    public virtual IServiceProvider GetServiceProvider() => ServiceProvider;

    /// <summary>
    /// Gets the scenario options.
    /// </summary>
    /// <returns></returns>
    public virtual IBaseScenarioOptions GetScenarioOptions() => ScenarioOptions;

    /// <summary>
    /// Gets the 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public virtual Mock<T> GetMock<T>() where T : class
    {
        if (Mocks.Count == 0)
            return null;

        foreach (KeyValuePair<Type, Mock> mock in Mocks)
        {
            if (mock.Key == typeof(T))
                return (Mock<T>)mock.Value;
        }

        return null;
    }

    /// <summary>
    /// Setups the mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public virtual void SetupMock<T>(Action<Mock<T>> action) where T : class
    {
        Mock<T> mock = GetMock<T>();
        action(mock);
    }

    /// <summary>
    /// Setups the mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="I"></typeparam>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public virtual void SetupMock<T, I>(Action<Mock<T>, I> action)
        where T : class
        where I : class, T
    {
        Mock<T> mock = GetMock<T>();
        action(mock, GetRequiredService<I>());
    }

    /// <summary>
    /// Verifies the mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action">The action.</param>
    public virtual void VerifyMock<T>(Action<Mock<T>> action) where T : class
    {
        Mock<T> mock = GetMock<T>();
        action(mock);
    }

    /// <summary>
    /// Injects once the anti forgery token.
    /// </summary>
    public virtual void EnsureAntiForgeryToken()
    {
        IHttpContextAccessor httpContextAccessor = GetRequiredService<IHttpContextAccessor>();
        EnsureAntiForgeryToken(httpContextAccessor.HttpContext);
    }

    /// <summary>
    /// Injects once the anti forgery token based in the <paramref name="httpContext"/>.
    /// </summary>
    public virtual void EnsureAntiForgeryToken(HttpContext httpContext)
    {
        IAntiforgery antiForgery = GetRequiredService<IAntiforgery>();

        AntiforgeryTokenSet antiForgeryToken = antiForgery.GetTokens(httpContext);
        AjaxRequestHandler.SetAntiForgeryToken(new AntiForgeryTokenOptions
        {
            HeaderName = antiForgeryToken.HeaderName,
            CookieName = GetOptions<AntiforgeryOptions>().Cookie.Name,
            FormFieldName = antiForgeryToken.FormFieldName,
            RequestToken = antiForgeryToken.RequestToken,
            CookieToken = antiForgeryToken.CookieToken
        });
    }

    /// <summary>
    /// Builds this instance.
    /// </summary>
    /// <exception cref="ApplicationException">ServiceProvider is already built</exception>
    protected virtual async Task BuildAsync()
    {
        if (ServiceProvider != null)
            throw new ApplicationException("ServiceProvider is already built");

        _webHostBuilder
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((_, configurationBuilder) =>
            {
                configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());

                foreach (KeyValuePair<string, bool> scenarioOptionsSetting in ScenarioOptions.JsonSettings)
                    configurationBuilder.AddJsonFile(scenarioOptionsSetting.Key, optional: scenarioOptionsSetting.Value, reloadOnChange: false);

                configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                configurationBuilder.AddJsonFile($"appsettings.{ScenarioOptions.Environment}.json", optional: true, reloadOnChange: false);
                configurationBuilder.AddJsonFile("appsettings.LocalTests.json", optional: true, reloadOnChange: false);

                if (ScenarioOptions.Settings.Count > 0)
                    configurationBuilder.AddInMemoryCollection(ScenarioOptions.Settings);
            })
            .ConfigureWebHost(webHost =>
            {
                webHost.UseStartup(ScenarioOptions.Startup);

                if (ScenarioOptions.HttpServer)
                    webHost.UseTestServer();
            })
            .UseEnvironment(ScenarioOptions.Environment)
            .ConfigureServices((hostBuilderContext, services) =>
            {
                foreach (Action<IServiceCollection, IConfiguration> action in ScenarioOptions.DatabaseServices)
                    action(services, hostBuilderContext.Configuration);

                foreach (Action<IServiceCollection> action in ScenarioOptions.Services)
                    action(services);
            });

        _webHost = _webHostBuilder.Build();

        if (ScenarioOptions.HttpServer)
        {
            await _webHost.StartAsync();
            TestServer = _webHost.GetTestServer();
            AjaxRequestHandler = new AjaxRequestHandler(TestServer.CreateClient());
        }

        ServiceProvider = _webHost.Services;

        foreach (Action<IServiceProvider> action in ScenarioOptions.AfterBuild)
            action(ServiceProvider);

        foreach (Func<IServiceProvider, Task> action in ScenarioOptions.AfterBuildAsync)
            await action(ServiceProvider);
    }

    /// <summary>
    /// Gets the entity builder.
    /// </summary>
    /// <returns></returns>
    public virtual BaseScenarioEntityBuilder GetEntityBuilder() => throw new NotImplementedException();

    /// <summary>
    /// Adds the entities asynchronously.
    /// </summary>
    /// <param name="scenarioEntityBuilder">The scenario entity builder.</param>
    /// <returns></returns>
    public abstract Task AddEntitiesAsync(BaseScenarioEntityBuilder scenarioEntityBuilder);

    /// <summary>
    /// Initializes asynchronously.
    /// </summary>
    protected virtual async Task InitAsync()
    {
        ICollection<IBeforeBuilderAsyncTask> beforeBuilderAsyncTasks = GetBeforeBuilderAsyncTasks();
        foreach (IBeforeBuilderAsyncTask scenarioOptionsBeforeBuilderAsyncTask in beforeBuilderAsyncTasks)
        {
            await scenarioOptionsBeforeBuilderAsyncTask.ExecuteBeforeAsync(this);
        }

        ICollection<IBeforeBuilderTask> beforeBuilderTasks = GetBeforeBuilderTasks();
        foreach (IBeforeBuilderTask scenarioOptionsBeforeBuilderTask in beforeBuilderTasks)
        {
            scenarioOptionsBeforeBuilderTask.ExecuteBefore(this);
        }

        await BuildAsync();
        await AddEntitiesAsync();

        ICollection<IAfterBuilderAsyncTask> afterBuilderAsyncTasks = GetAfterBuilderAsyncTasks();
        foreach (IAfterBuilderAsyncTask scenarioOptionsAfterBuilderTask in afterBuilderAsyncTasks)
        {
            await scenarioOptionsAfterBuilderTask.ExecuteAfterAsync(this);
        }

        ICollection<IAfterBuilderTask> afterBuilderTasks = GetAfterBuilderTasks();
        foreach (IAfterBuilderTask scenarioOptionsAfterBuilderTask in afterBuilderTasks)
        {
            scenarioOptionsAfterBuilderTask.ExecuteAfter(this);
        }

        SetupUrls();
    }

    /// <summary>
    /// Gets a list of <see cref="IBeforeBuilderAsyncTask"/>.
    /// </summary>
    /// <returns></returns>
    protected virtual ICollection<IBeforeBuilderAsyncTask> GetBeforeBuilderAsyncTasks() => ScenarioOptions.TaskBuilderOptions.BeforeBuilderAsyncTasks;

    /// <summary>
    /// Gets a list of <see cref="IBeforeBuilderTask"/>.
    /// </summary>
    /// <returns></returns>
    protected virtual ICollection<IBeforeBuilderTask> GetBeforeBuilderTasks() => ScenarioOptions.TaskBuilderOptions.BeforeBuilderTasks;

    /// <summary>
    /// Gets a list of <see cref="IAfterBuilderAsyncTask"/>.
    /// </summary>
    /// <returns></returns>
    protected virtual ICollection<IAfterBuilderAsyncTask> GetAfterBuilderAsyncTasks() => ScenarioOptions.TaskBuilderOptions.AfterBuilderAsyncTasks;

    /// <summary>
    /// Gets a list of <see cref="IAfterBuilderTask"/>.
    /// </summary>
    /// <returns></returns>
    protected virtual ICollection<IAfterBuilderTask> GetAfterBuilderTasks() => ScenarioOptions.TaskBuilderOptions.AfterBuilderTasks;

    /// <summary>
    /// Sets the <see cref="BaseScenario.Url"/> to mimic the Asp Net Core url framework.
    /// </summary>
    protected virtual void SetupUrls()
    {
        Url.Setup(t => t.Action(It.IsAny<UrlActionContext>())).Returns((UrlActionContext uac) =>
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append((string)$"{uac.Controller}/{uac.Action}");

            if (!string.IsNullOrEmpty(uac.Fragment))
            {
                stringBuilder.Append((string)$"#{uac.Fragment}");
            }

            if (uac.Values != null)
            {
                string values = string.Join("&", new RouteValueDictionary(uac.Values).Select(p => p.Key + "=" + p.Value));
                stringBuilder.Append("/" + values);
            }

            if (stringBuilder[0] != '/')
                stringBuilder.Insert(0, '/');

            string url = stringBuilder.ToString();

            return url;
        });

        Url.Setup(t => t.IsLocalUrl(It.IsAny<string>())).Returns((string url) =>
        {
            if (string.IsNullOrEmpty(url))
                return false;

            // Allows "/" or "/foo" but not "//" or "/\".
            if (url[0] == '/')
            {
                // url is exactly "/"
                if (url.Length == 1)
                {
                    return true;
                }

                // url doesn't start with "//" or "/\"
                if (url[1] != '/' && url[1] != '\\')
                {
                    return !HasControlCharacter(url.AsSpan(1));
                }

                return false;
            }

            // Allows "~/" or "~/foo" but not "~//" or "~/\".
            if (url[0] == '~' && url.Length > 1 && url[1] == '/')
            {
                // url is exactly "~/"
                if (url.Length == 2)
                {
                    return true;
                }

                // url doesn't start with "~//" or "~/\"
                if (url[2] != '/' && url[2] != '\\')
                {
                    return !HasControlCharacter(url.AsSpan(2));
                }

                return false;
            }

            return false;

            static bool HasControlCharacter(ReadOnlySpan<char> readOnlySpan)
            {
                // URLs may not contain ASCII control characters.
                foreach (char t in readOnlySpan)
                {
                    if (char.IsControl(t))
                    {
                        return true;
                    }
                }

                return false;
            }
        });
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;

        TestServer?.Dispose();
        _webHost?.Dispose();
        TestServer = null;
        _webHost = null;

        foreach (Action action in ScenarioOptions.WhenDispose)
            action();
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsyncCore()
    {
        TestServer?.Dispose();
        _webHost?.Dispose();

        TestServer = null;
        _webHost = null;

        foreach (Action action in ScenarioOptions.WhenDispose)
            action();

        foreach (Func<Task> action in ScenarioOptions.WhenDisposeAsync)
            await action().ConfigureAwait(false);
    }

    private async Task AddEntitiesAsync()
    {
        if (ScenarioOptions.ScenarioType == ScenarioType.NoData)
            return;

        await AddEntitiesAsync(GetEntityBuilder());
    }
}