using Microsoft.Extensions.Configuration;

namespace Carneiro.Core.Cache;

/// <summary>
/// The <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <param name="services"></param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds the database cache.
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        public IServiceCollection AddDbCache<TInterface, T, TDbContext>()
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
        /// <param name="configurationSection"></param>
        /// <typeparam name="TEntityCacheOptions"></typeparam>
        public IServiceCollection AddDbCacheConfiguration<TEntityCacheOptions>(IConfigurationSection configurationSection)
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
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        public IServiceCollection AddDbCacheRefresher<TInterface, T, TDbContext>()
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
}