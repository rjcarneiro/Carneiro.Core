using Carneiro.Core.Repository.Abstractions;
using Carneiro.Core.Repository.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
    /// <returns></returns>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, string connectionString)
        where T : DbContext => services.RegisterDatabaseAndUnitOfWork<IUnitOfWork, UnitOfWork<T>, T>(connectionString, new DatabaseOptions());

    /// <summary>
    /// Registers the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="actions">The actions.</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, string connectionString, Action<DatabaseOptions> actions)
        where T : DbContext
    {
        var dbSettings = new DatabaseOptions();
        actions.Invoke(dbSettings);

        return services.RegisterDatabaseAndUnitOfWork<IUnitOfWork, UnitOfWork<T>, T>(connectionString, dbSettings);
    }

    /// <summary>
    /// Registers the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="databaseOptions">The database options.</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions)
        where T : DbContext => services.RegisterDatabaseAndUnitOfWork<IUnitOfWork, UnitOfWork<T>, T>(connectionString, databaseOptions);

    /// <summary>
    /// Registers the database with a default connection string.
    /// </summary>
    /// <typeparam name="T">Generic that must be a <see cref="DbContext" />.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="actions">The actions.</param>
    /// <param name="serviceLifetime">The service lifetime. Default is <see cref="ServiceLifetime.Scoped"/></param>
    /// <returns></returns>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, string connectionString, Action<DatabaseOptions> actions, ServiceLifetime serviceLifetime)
        where T : DbContext
    {
        var dbSettings = new DatabaseOptions();
        actions?.Invoke(dbSettings);

        return services.RegisterDatabaseAndUnitOfWork<IUnitOfWork, UnitOfWork<T>, T>(connectionString, dbSettings, serviceLifetime);
    }

    /// <summary>
    /// Registers the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="databaseOptions">The database options.</param>
    /// <param name="serviceLifetime">The service lifetime. Default is <see cref="ServiceLifetime.Scoped"/></param>
    /// <returns></returns>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions, ServiceLifetime serviceLifetime)
        where T : DbContext
    {
        DatabaseOptions databaseSettings = databaseOptions ?? new DatabaseOptions();

        return services.RegisterDatabaseAndUnitOfWork<IUnitOfWork, UnitOfWork<T>, T>(connectionString, databaseSettings, serviceLifetime);
    }

    /// <summary>
    /// Registers the database that expects <c>DatabaseContext</c> as a connection string configuration section.
    /// </summary>
    /// <typeparam name="T">Generic that must be a <see cref="DbContext" />.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="serviceLifetime">The service lifetime. Default is <see cref="ServiceLifetime.Scoped"/></param>
    /// <returns></returns>
    /// <remarks>
    /// It expects <c>DatabaseContext</c> as configuration section for the connection string.
    /// It expects <c>Database</c> as configuration for <see cref="DatabaseOptions" />.
    /// </remarks>
    public static IServiceCollection AddDatabase<T>(this IServiceCollection services, IConfiguration configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        where T : DbContext => services.RegisterDatabaseAndUnitOfWork<IUnitOfWork, UnitOfWork<T>, T>(configuration.GetConnectionString("DatabaseContext"), configuration.GetSection("Database").Get<DatabaseOptions>(), serviceLifetime);

    /// <summary>
    /// Registers a basic <see cref="ServiceLifetime.Scoped"/> implementation of <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services)
        where T : DbContext => services.AddUnitOfWork<T>(ServiceLifetime.Scoped);

    /// <summary>
    /// Registers a basic <paramref name="serviceLifetime"/> implementation of <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="serviceLifetime">The service lifetime.</param>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services, ServiceLifetime serviceLifetime)
        where T : DbContext
    {
        services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork<T>), serviceLifetime));
        return services;
    }

    private static IServiceCollection RegisterDatabaseAndUnitOfWork<TInterface, TImplementation, TDbContext>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        where TInterface : IUnitOfWork
        where TImplementation : class, TInterface
        where TDbContext : DbContext
    {

        services.RegisterDatabaseContext<TDbContext>(connectionString, databaseOptions, serviceLifetime);
        services.AddUnitOfWork<TDbContext>(serviceLifetime);

        return services;
    }

    private static IServiceCollection RegisterDatabaseContext<TDbContext>(this IServiceCollection services, string connectionString, DatabaseOptions databaseOptions, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped, Action<DbContextOptionsBuilder> dbContextOptionsBuilder = null)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(options =>
        {
            options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            options.UseSqlServer(connectionString, providerOptions =>
            {
                providerOptions.CommandTimeout(databaseOptions.Timeout);
                providerOptions.EnableRetryOnFailure(
                    maxRetryCount: databaseOptions.Failure.Retries,
                    maxRetryDelay: TimeSpan.FromSeconds(databaseOptions.Failure.Seconds),
                    errorNumbersToAdd: null);
            });
            dbContextOptionsBuilder?.Invoke(options);
        }, contextLifetime: serviceLifetime, optionsLifetime: serviceLifetime);
        return services;
    }
}