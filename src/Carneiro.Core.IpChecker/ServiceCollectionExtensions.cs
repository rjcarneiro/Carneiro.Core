using Microsoft.Extensions.DependencyInjection;

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
    /// <remarks>It uses <c>IpAddressCheckerOptions</c> as section configuration name.</remarks>
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

        services.AddHttpClient<IfConfigIpAddressHttpClient>()
            .ConfigureHttpClient(client => { client.BaseAddress = new Uri("https://ifconfig.me"); });

        services.AddHttpClient<IpIfyIpAddressHttpClient>()
            .ConfigureHttpClient(client => { client.BaseAddress = new Uri("https://api.ipify.org"); });

        return services;
    }
}