using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Carneiro.Core.Repository.Extensions;

/// <summary>
/// Data Protection extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class DataProtectionExtensions
{
    /// <summary>
    /// Adds data protection keys to database <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddRzDataProtection<T>(this IServiceCollection services) where T : DbContext, IDataProtectionKeyContext
    {
        services.AddDataProtection()
            .PersistKeysToDbContext<T>();

        return services;
    }

    /// <summary>
    /// Adds data protection keys to database <typeparamref name="T"/> with application name <paramref name="appName"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="appName">Name of the application.</param>
    /// <returns></returns>
    public static IServiceCollection AddRzDataProtection<T>(this IServiceCollection services, string appName) where T : DbContext, IDataProtectionKeyContext
    {
        services.AddDataProtection()
            .PersistKeysToDbContext<T>()
            .SetApplicationName(appName);

        return services;
    }

    /// <summary>
    /// Adds data protection keys to database <typeparamref name="T"/> with default key life time <paramref name="keyLifeTime"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="keyLifeTime">The key life time.</param>
    /// <returns></returns>
    public static IServiceCollection AddRzDataProtection<T>(this IServiceCollection services, TimeSpan keyLifeTime) where T : DbContext, IDataProtectionKeyContext
    {
        services.AddDataProtection()
            .PersistKeysToDbContext<T>()
            .SetDefaultKeyLifetime(keyLifeTime);

        return services;
    }

    /// <summary>
    /// Adds data protection keys to database <typeparamref name="T"/> with application name <paramref name="appName"/> and default key life time <paramref name="keyLifeTime"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <param name="appName">Name of the application.</param>
    /// <param name="keyLifeTime">The key lie time.</param>
    /// <returns></returns>
    public static IServiceCollection AddRzDataProtection<T>(this IServiceCollection services, string appName, TimeSpan keyLifeTime) where T : DbContext, IDataProtectionKeyContext
    {
        services.AddDataProtection()
            .PersistKeysToDbContext<T>()
            .SetApplicationName(appName)
            .SetDefaultKeyLifetime(keyLifeTime);

        return services;
    }
}