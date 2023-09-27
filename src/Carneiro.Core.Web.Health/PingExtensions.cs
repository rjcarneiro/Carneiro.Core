using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Carneiro.Core.Web.Health;

/// <summary>
/// Extensions for ping.
/// </summary>
public static class PingExtensions
{
    /// <summary>
    /// Adds the ping controller using <c>PingOptions</c> as section.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddPingController(this IServiceCollection services, IConfiguration configuration) 
        => services.AddPingController(configuration.GetSection("PingOptions"));

    /// <summary>
    /// Adds the ping controller using <paramref name="configurationSection"/> section as options.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection"></param>
    public static IServiceCollection AddPingController(this IServiceCollection services, IConfigurationSection configurationSection) 
        => services.AddPingController(configurationSection.Get<PingOptions>() ?? new PingOptions());

    /// <summary>
    /// Adds the ping controller using <paramref name="action"/> as options.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    public static IServiceCollection AddPingController(this IServiceCollection services, Action<PingOptions> action)
    {
        var pingOptions = new PingOptions();

        action?.Invoke(pingOptions);

        return services.AddPingController(pingOptions);
    }

    /// <summary>
    /// Adds the ping controller using <paramref name="pingOptions"/> as options.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="pingOptions"></param>
    public static IServiceCollection AddPingController(this IServiceCollection services, PingOptions pingOptions)
    {
        services.Configure<PingOptions>(o =>
        {
            o.Route = pingOptions.Route;
            o.ShowVersion = pingOptions.ShowVersion;
        });
        services.AddSingleton(_ => VersionHelper.GetVersion());

        return services;
    }
}