namespace Carneiro.Core.Repository.Extensions;

/// <summary>
/// Class that contains extension methods for <see cref="IServiceCollection"/> to support database registration.
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Adds the database that expects  <paramref name="connectionString"/> as connection string.
    /// </summary>
    /// <typeparam name="T">Generic that must be a <see cref="DbContext" />.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, string connectionString)
        where T : DbContext => services.AddDatabaseContext<T>(connectionString, new DatabaseOptions());

    /// <summary>
    /// Adds the database that expects  <paramref name="connectionString"/> as connection string.
    /// </summary>
    /// <typeparam name="T">Generic that must be a <see cref="DbContext" />.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="dbContextOptionsBuilder">The database context options builder action.</param>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, string connectionString, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
        where T : DbContext => services.AddDatabaseContext<T>(connectionString, new DatabaseOptions(), dbContextOptionsBuilder);

    /// <summary>
    /// Adds the database that expects  <paramref name="connectionString"/> as connection string and <paramref name="databaseOptionsSection"/> as <see cref="DatabaseOptions"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <param name="databaseOptionsSection"></param>
    /// <typeparam name="T"></typeparam>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, string connectionString, IConfigurationSection databaseOptionsSection)
        where T : DbContext => services.AddDatabaseContext<T>(connectionString, databaseOptionsSection.Get<DatabaseOptions>());

    /// <summary>
    /// Adds the database that expects  <paramref name="connectionString"/> as connection string and <paramref name="databaseOptionsSection"/> as <see cref="DatabaseOptions"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <param name="databaseOptionsSection"></param>
    /// <param name="dbContextOptionsBuilder">The database context options builder action.</param>
    /// <typeparam name="T"></typeparam>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, string connectionString, IConfigurationSection databaseOptionsSection, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
        where T : DbContext =>
        services.AddDatabaseContext<T>(connectionString, databaseOptionsSection.Get<DatabaseOptions>(), dbContextOptionsBuilder);

    /// <summary>
    /// Adds the database that expects  <paramref name="connectionString"/> as connection string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="action">The actions.</param>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, string connectionString, Action<DatabaseOptions> action)
        where T : DbContext
    {
        var dbSettings = new DatabaseOptions();
        action.Invoke(dbSettings);

        return services.AddDatabaseContext<T>(connectionString, dbSettings);
    }

    /// <summary>
    /// Adds the database that expects  <paramref name="connectionString"/> as connection string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="action">The actions.</param>
    /// <param name="dbContextOptionsBuilder">The database context options builder action.</param>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, string connectionString, Action<DatabaseOptions> action, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
        where T : DbContext
    {
        var dbSettings = new DatabaseOptions();
        action.Invoke(dbSettings);

        return services.AddDatabaseContext<T>(connectionString, dbSettings, dbContextOptionsBuilder);
    }

    /// <summary>
    /// Adds the database that expects <c>DatabaseContext</c> as a connection string configuration section.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="databaseOptions">The database options.</param>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions)
        where T : DbContext =>
        services.AddDatabaseContext<T>(connectionString, databaseOptions);

    /// <summary>
    /// Adds the database that expects <c>DatabaseContext</c> as a connection string configuration section.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="databaseOptions">The database options.</param>
    /// <param name="dbContextOptionsBuilder">The database context options builder action.</param>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
        where T : DbContext =>
        services.AddDatabaseContext<T>(connectionString, databaseOptions, dbContextOptionsBuilder);

    /// <summary>
    /// Adds the database that expects <c>DatabaseContext</c> as a connection string configuration section and <c>Database</c> as configuration section for <see cref="DatabaseOptions"/>.
    /// </summary>
    /// <typeparam name="T">Generic that must be a <see cref="DbContext" />.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <remarks>
    /// It expects <c>DatabaseContext</c> as configuration section for the connection string.
    /// It expects <c>Database</c> as configuration for <see cref="DatabaseOptions" />.
    /// </remarks>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, IConfiguration configuration)
        where T : DbContext =>
        services.AddDatabaseContext<T>(configuration.GetConnectionString("DatabaseContext"), configuration.GetSection("Database").Get<DatabaseOptions>());

    /// <summary>
    /// Adds the database that expects <c>DatabaseContext</c> as a connection string configuration section and <c>Database</c> as configuration section for <see cref="DatabaseOptions"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="dbContextOptionsBuilder">The database context options builder action.</param>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// It expects <c>DatabaseContext</c> as configuration section for the connection string.
    /// It expects <c>Database</c> as configuration for <see cref="DatabaseOptions" />.
    /// </remarks>
    public static IDatabaseBuilder AddDatabase<T>(this IServiceCollection services, IConfiguration configuration, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
        where T : DbContext =>
        services.AddDatabaseContext<T>(configuration.GetConnectionString("DatabaseContext"), configuration.GetSection("Database").Get<DatabaseOptions>(), dbContextOptionsBuilder);

    private static IDatabaseBuilder AddDatabaseContext<TDbContext>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions, Action<DbContextOptionsBuilder> dbContextOptionsBuilder = null)
        where TDbContext : DbContext
    {
        Action<DbContextOptionsBuilder> d = options =>
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
        };

        if (databaseOptions.UseDbContextPool)
        {
            services.AddDbContextPool<TDbContext>(d);
        }
        else
        {
            services.AddDbContext<TDbContext>(d);
        }

        return new DatabaseBuilder(services, databaseOptions);
    }
}