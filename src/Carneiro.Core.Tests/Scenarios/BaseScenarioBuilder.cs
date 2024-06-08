using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using Carneiro.Core.Repository;
using Carneiro.Core.Repository.Abstractions;
using Carneiro.Core.Repository.Options;
using Carneiro.Core.Tests.Generators;
using Carneiro.Core.Tests.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Carneiro.Core.Tests.Scenarios;

/// <summary>
/// Base scenario builder.
/// </summary>
/// <seealso cref="IBaseScenarioBuilder" />
public abstract class BaseScenarioBuilder : IBaseScenarioBuilder
{
    /// <summary>
    /// Gets the options.
    /// </summary>
    /// <value>
    /// The options.
    /// </value>
    protected virtual IBaseScenarioOptions Options { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseScenarioBuilder"/> class.
    /// </summary>
    /// <param name="scenarioOptions">The scenario options.</param>
    protected BaseScenarioBuilder(IBaseScenarioOptions scenarioOptions)
    {
        Options = scenarioOptions;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder UseInMemoryDatabase<T>() where T : RzDbContext => Use<T>(ScenarioType.InMemory);

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder UseSqlLite<T>() where T : RzDbContext => Use<T>(ScenarioType.SqlLite);

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder UseSqlServer<T>() where T : RzDbContext => Use<T>(ScenarioType.SqlServer);

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder UseSqlLiteInMemory<T>() where T : RzDbContext => Use<T>(ScenarioType.SqlLiteInMemory);

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder UseNoData<T>() where T : RzDbContext => Use<T>(ScenarioType.NoData);

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder StartHttpServer()
    {
        Options.HttpServer = true;
        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithService(Action<IServiceCollection> service)
    {
        Options.Services.Add(service);
        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder OfStartUp<T>() where T : class
    {
        Options.Startup = typeof(T);
        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithEnvironment(string environment)
    {
        Options.Environment = environment;
        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithDatabaseOptions(Action<DatabaseOptions> action)
    {
        Options.DatabaseOptions = action;
        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WhenDispose(Action action)
    {
        Options.WhenDispose.Add(action);
        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder AfterBuild(Action<IServiceProvider> action)
    {
        Options.AfterBuild.Add(action);
        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithSetting(string key, string value)
    {
        Options.Settings.Add(key, value);
        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithSetting(string key, bool value) => WithSetting(key, value.ToString());

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithSetting(string key, int value) => WithSetting(key, value.ToString());

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithSetting(string key, long value) => WithSetting(key, value.ToString());

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithSetting(string key, decimal value) => WithSetting(key, value.ToString(CultureInfo.InvariantCulture));

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithMock<T>() where T : class => WithMock<T>(action: null);

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithMock<T>(Action<Mock<T>> action) where T : class
    {
        if (Options.Mocks.ContainsKey(typeof(T)))
            Options.Mocks.Remove(typeof(T));

        var mock = new Mock<T>();

        Options.Mocks.Add(typeof(T), mock);
        Options.Services.Add(s => s.AddTransient<T>(_ => mock.Object));

        action?.Invoke(mock);

        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithMock<T, I>()
        where T : class
        where I : class, T
    {
        if (Options.Mocks.ContainsKey(typeof(T)))
            Options.Mocks.Remove(typeof(T));

        var mockedImplementation = new Mock<T>();

        Options.Mocks.Add(typeof(T), mockedImplementation);
        Options.Services.Add(s => s.AddTransient<T>(_ => mockedImplementation.Object));
        Options.Services.Add(s => s.AddTransient<I>());

        return this;
    }

    /// <inheritdoc />
    public virtual IBaseScenarioBuilder WithFakeAntiForgery() => WithMock<IAntiforgery>(mock =>
    {
        const string UselessValue = "hello";
        var tokenSet = new AntiforgeryTokenSet(UselessValue, UselessValue, UselessValue, UselessValue);
        mock.Setup(t => t.GetAndStoreTokens(It.IsAny<HttpContext>())).Returns(tokenSet);
        mock.Setup(t => t.GetTokens(It.IsAny<HttpContext>())).Returns(tokenSet);
        mock.Setup(t => t.IsRequestValidAsync(It.IsAny<HttpContext>())).ReturnsAsync(true);
        mock.Setup(t => t.ValidateRequestAsync(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);
    });

    /// <inheritdoc />
    public virtual ITaskBuilder TaskBuilder { get; } = new TaskBuilder();

    /// <inheritdoc />
    public virtual IBaseScenarioOptions Build()
    {
        Options.TaskBuilderOptions = TaskBuilder.Build();
        return Options;
    }

    private IBaseScenarioBuilder Use<T>(ScenarioType scenarioType) where T : RzDbContext
    {
        Options.ScenarioType = scenarioType;

        if (Options.ScenarioType == ScenarioType.NoData)
        {
            Options.Services.Add(s => s.Add(new ServiceDescriptor(typeof(T), _ => new Mock<T>().Object, ServiceLifetime.Scoped)));
            Options.Services.Add(s => s.Add(new ServiceDescriptor(typeof(IUnitOfWork), _ => new Mock<IUnitOfWork>().Object, ServiceLifetime.Scoped)));
            Options.Services.Add(s => s.Add(new ServiceDescriptor(typeof(ITransactionalUnitOfWork), _ => new Mock<ITransactionalUnitOfWork>().Object, ServiceLifetime.Scoped)));
        }
        else
        {
            Options.DatabaseServices.Add(AddDatabaseStrategy<T>());
            Options.Services.Add(s => s.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork<T>), ServiceLifetime.Scoped)));
            Options.Services.Add(s => s.Add(new ServiceDescriptor(typeof(ITransactionalUnitOfWork), typeof(TransactionalUnitOfWork<T>), ServiceLifetime.Scoped)));
        }

        return this;
    }

    private Action<IServiceCollection, IConfiguration> AddDatabaseStrategy<T>() where T : DbContext => (services, configuration) =>
    {
        var faker = new Faker();
        DatabaseOptions dbOptions = configuration.GetSection("Database").Get<DatabaseOptions>() ?? new DatabaseOptions();
        Options.DatabaseOptions?.Invoke(dbOptions);

        var connectionString = configuration.GetConnectionString("DatabaseContext");
        DbConnection connection = null;

        var databaseBuilderActions = new List<Action<DbContextOptionsBuilder>>
        {
            options =>
            {
                options.EnableDetailedErrors(dbOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(dbOptions.EnableSensitiveDataLogging);
            }
        };

        Action<DbContextOptionsBuilder> databaseBuilder = Options.ScenarioType switch
        {
            ScenarioType.InMemory => options =>
            {
                options.UseInMemoryDatabase(databaseName: faker.GenerateUniqueId().ToUpper());
                options.ConfigureWarnings(warnings =>
                {
                    warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                });
            }
            ,
            ScenarioType.SqlServer => options =>
            {
                options.UseSqlServer(connectionString, providerOptions =>
                {
                    if (dbOptions.QuerySplittingBehavior.HasValue)
                        providerOptions.UseQuerySplittingBehavior(dbOptions.QuerySplittingBehavior.Value);

                    if (dbOptions.UseRelationalNulls.HasValue)
                        providerOptions.UseRelationalNulls(dbOptions.UseRelationalNulls.Value);

                    if (dbOptions.MinBatchSize.HasValue)
                        providerOptions.MinBatchSize(dbOptions.MinBatchSize.Value);

                    if (dbOptions.MaxBatchSize.HasValue)
                        providerOptions.MaxBatchSize(dbOptions.MaxBatchSize.Value);

                    providerOptions.CommandTimeout(dbOptions.Timeout);
                    providerOptions.EnableRetryOnFailure(
                        maxRetryCount: dbOptions.Failure.Retries,
                        maxRetryDelay: TimeSpan.FromSeconds(dbOptions.Failure.Seconds),
                        errorNumbersToAdd: null);
                });
            }
            ,
            ScenarioType.SqlLite => options =>
            {
                const string sqlLiteFolder = "sqlite";

                if (!Directory.Exists(sqlLiteFolder))
                    Directory.CreateDirectory(sqlLiteFolder);

                // save the relative path to delete it in the end
                connectionString = $@"{sqlLiteFolder}\{faker.GetTicks()}-{faker.GenerateUniqueId().ToUpper()}.db";

                var connectionStringBuilder = new SqliteConnectionStringBuilder
                {
                    DataSource = connectionString,
                    Mode = SqliteOpenMode.ReadWriteCreate,
                    Cache = SqliteCacheMode.Shared
                };

                connection = new SqliteConnection(connectionStringBuilder.ToString());
                connection.Open();

                options.UseSqlite(connection, o =>
                {
                    o.CommandTimeout(dbOptions.Timeout);
                });
            }
            ,
            ScenarioType.SqlLiteInMemory => options =>
            {
                connection = new SqliteConnection("Filename=:memory:");
                connection.Open();
                options.UseSqlite(connection);
            }
            ,
            _ => throw new InvalidOperationException($"Cannot resolve database specs for {Options.ScenarioType}")
        };

        databaseBuilderActions.Add(databaseBuilder);

        services.AddDbContext<T>(options =>
        {
            foreach (Action<DbContextOptionsBuilder> databaseBuilderAction in databaseBuilderActions)
            {
                databaseBuilderAction(options);
            }
        });

        if (Options.ScenarioType is ScenarioType.SqlLite or ScenarioType.SqlLiteInMemory)
        {
            Options.AfterBuildAsync.Add(async serviceProvider =>
            {
                T dbContext = serviceProvider.GetRequiredService<T>();
                if (await dbContext.Database.CanConnectAsync())
                {
                    await dbContext.Database.EnsureCreatedAsync();
                }
            });

            Options.WhenDisposeAsync.Add(async () =>
            {
                try
                {
                    Debug.Assert(connection != null, nameof(connection) + " != null");
                    await connection.CloseAsync()!;
                    await connection.DisposeAsync()!;

                    File.Delete(connectionString);
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Unable to delete {connectionString}. Reason: {e.GetBaseException().Message}");
                }
            });
        }
    };
}