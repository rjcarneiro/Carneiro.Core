namespace Carneiro.Core.Repository.Extensions;

/// <summary>
/// Class that contains extension methods for <see cref="IServiceCollection"/> to support database registration.
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Registers the database with a default connection string.
    /// </summary>
    /// <typeparam name="T">Generic that must be a <see cref="DbContext" />.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, string connectionString) where T : DbContext
    {
        return services.RegisterDatabaseAndUnitOfWork<T>(connectionString, new DatabaseOptions());
    }

    /// <summary>
    /// Registers the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="actions">The actions.</param>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, string connectionString, Action<DatabaseOptions> actions)
        where T : DbContext
    {
        var dbSettings = new DatabaseOptions();
        actions.Invoke(dbSettings);

        return services.RegisterDatabaseAndUnitOfWork<T>(connectionString, dbSettings);
    }

    /// <summary>
    /// Registers the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="databaseOptions">The database options.</param>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions) where T : DbContext
    {
        return services.RegisterDatabaseAndUnitOfWork<T>(connectionString, databaseOptions);
    }

    /// <summary>
    /// Adds the database that expects <c>DatabaseContext</c> as a connection string configuration section.
    /// </summary>
    /// <typeparam name="T">Generic that must be a <see cref="DbContext" />.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <remarks>
    /// It expects <c>DatabaseContext</c> as configuration section for the connection string.
    /// It expects <c>Database</c> as configuration for <see cref="DatabaseOptions" />.
    /// </remarks>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        services.RegisterDatabaseAndUnitOfWork<T>(configuration.GetConnectionString("DatabaseContext"), configuration.GetSection("Database").Get<DatabaseOptions>());

        return services;
    }

    /// <summary>
    /// Registers a basic <see cref="ServiceLifetime.Scoped"/> implementation of <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <param name="services">The services.</param>
    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : DbContext
    {
        services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork<T>), ServiceLifetime.Scoped));
        return services;
    }

    private static IServiceCollection RegisterDatabaseAndUnitOfWork<TDbContext>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions) where TDbContext : DbContext
    {
        services.RegisterDatabaseContext<TDbContext>(connectionString, databaseOptions);
        services.AddUnitOfWork<TDbContext>();

        return services;
    }

    private static IServiceCollection RegisterDatabaseContext<TDbContext>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions, Action<DbContextOptionsBuilder> dbContextOptionsBuilder = null)
        where TDbContext : DbContext
    {
        services.AddDbContextPool<TDbContext>(options =>
        {
            options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            options.UseSqlServer(connectionString, providerOptions =>
            {
                if (databaseOptions.QuerySplittingBehavior.HasValue)
                    providerOptions.UseQuerySplittingBehavior(databaseOptions.QuerySplittingBehavior.Value);

                if (databaseOptions.UseRelationalNulls.HasValue)
                    providerOptions.UseRelationalNulls(databaseOptions.UseRelationalNulls.Value);

                if (databaseOptions.MinBatchSize.HasValue)
                    providerOptions.MinBatchSize(databaseOptions.MinBatchSize.Value);

                if (databaseOptions.MaxBatchSize.HasValue)
                    providerOptions.MaxBatchSize(databaseOptions.MaxBatchSize.Value);

                providerOptions.CommandTimeout(databaseOptions.Timeout);
                providerOptions.EnableRetryOnFailure(
                    maxRetryCount: databaseOptions.Failure.Retries,
                    maxRetryDelay: TimeSpan.FromSeconds(databaseOptions.Failure.Seconds),
                    errorNumbersToAdd: null);
            });
            dbContextOptionsBuilder?.Invoke(options);
        });
        return services;
    }
}