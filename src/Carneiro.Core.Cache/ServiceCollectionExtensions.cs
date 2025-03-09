using Microsoft.Extensions.Configuration;

namespace Carneiro.Core.Cache;

/// <summary>
/// The <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the database cache.
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public static IServiceCollection AddDbCache<TInterface, T, TDbContext>(this IServiceCollection services)
        where TInterface : class
        where T : class, TInterface, ISingletonEntityCache<TDbContext>
        where TDbContext : DbContext
    {
        services.AddSingleton<T>();
        services.AddSingleton<TInterface, T>(sp => sp.GetRequiredService<T>());
        services.AddSingleton<ISingletonEntityCache<TDbContext>>(sp => sp.GetRequiredService<T>());

        services.AddHostedService<CacheRefreshService<T, TDbContext>>();
        services.AddSingleton<IAsyncInitializer, GenericCacheInitializer<TDbContext>>();

        return services;
    }

    /// <summary>
    /// Adds the database cache configuration.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection"></param>
    /// <typeparam name="TEntityCacheOptions"></typeparam>
    public static IServiceCollection AddDbCacheConfiguration<TEntityCacheOptions>(this IServiceCollection services, IConfigurationSection configurationSection)
        where TEntityCacheOptions : EntityCacheOptions
    {
        TEntityCacheOptions options = configurationSection.Get<TEntityCacheOptions>();
        services.Configure<TEntityCacheOptions>(o =>
        {
            o.CacheDuration = options.CacheDuration;
        });

        return services;
    }

    /// <summary>
    /// Adds the database cache refresher job.
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public static IServiceCollection AddDbCacheRefresher<TInterface, T, TDbContext>(this IServiceCollection services)
        where TInterface : class
        where T : class, TInterface, IDatabaseCacheRefresher<TDbContext>
        where TDbContext : DbContext
    {
        services.AddSingleton<T>();
        services.AddSingleton<TInterface, T>(sp => sp.GetRequiredService<T>());
        services.AddSingleton<IDatabaseCacheRefresher<TDbContext>>(sp => sp.GetRequiredService<T>());
        services.AddSingleton<IDatabaseCacheRefresher>(sp => sp.GetRequiredService<T>());

        return services;
    }
}