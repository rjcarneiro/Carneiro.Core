using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Carneiro.Core.IpChecker;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Ip checker interface with its dependencies.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <remarks>It uses <c>IpAddressCheckerOptions</c> section configuration name.</remarks>
    public static IServiceCollection AddIpChecker(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddIpChecker(configuration.GetSection(nameof(IpAddressCheckerOptions)));
    }

    /// <summary>
    /// Adds the Ip checker interface with its dependencies.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection"></param>
    public static IServiceCollection AddIpChecker(this IServiceCollection services, IConfigurationSection configurationSection)
    {
        return services.AddIpChecker(configurationSection.Get<IpAddressCheckerOptions>());
    }

    /// <summary>
    /// Adds the Ip checker interface with its dependencies.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    public static IServiceCollection AddIpChecker(this IServiceCollection services, Action<IpAddressCheckerOptions> action)
    {
        var options = new IpAddressCheckerOptions();
        action(options);

        return services.AddIpChecker(options);
    }

    /// <summary>
    /// Adds the Ip checker interface with its dependencies.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    public static IServiceCollection AddIpChecker(this IServiceCollection services, IpAddressCheckerOptions options)
    {
        services.Configure<IpAddressCheckerOptions>(o =>
        {
            o.PersistenceCacheTimeout = options.PersistenceCacheTimeout;
            o.UsePersistenceCache = options.UsePersistenceCache;
        });

        services.AddScoped<IIpAddressChecker, IpAddressChecker>();
        services.TryAddEnumerable(new ServiceDescriptor(typeof(IIpAddressHttpClient), typeof(IfConfigIpAddressHttpClient), ServiceLifetime.Scoped));
        services.TryAddEnumerable(new ServiceDescriptor(typeof(IIpAddressHttpClient), typeof(IpIfyIpAddressHttpClient), ServiceLifetime.Scoped));

        return services;
    }
}