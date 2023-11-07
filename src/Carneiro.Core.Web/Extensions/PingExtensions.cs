using Microsoft.Extensions.Configuration;

namespace Carneiro.Core.Web.Extensions;

/// <summary>
/// <see cref="IServiceCollection"/> for <see cref="VersionModel"/>.
/// </summary>
public static class PingExtensions
{
    /// <summary>
    /// Adds the <see cref="VersionModel"/> using <c>PingOptions</c> as section.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddPing(this IServiceCollection services, IConfiguration configuration) 
        => services.AddPing(configuration.GetSection("PingOptions"));

    /// <summary>
    /// Adds the <see cref="VersionModel"/> using <paramref name="configurationSection"/> section as options.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection"></param>
    public static IServiceCollection AddPing(this IServiceCollection services, IConfigurationSection configurationSection) 
        => services.AddPing(configurationSection.Get<PingOptions>() ?? new PingOptions());

    /// <summary>
    /// Adds the <see cref="VersionModel"/> using <paramref name="action"/> as options.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    public static IServiceCollection AddPing(this IServiceCollection services, Action<PingOptions> action)
    {
        var pingOptions = new PingOptions();

        action?.Invoke(pingOptions);

        return services.AddPing(pingOptions);
    }

    /// <summary>
    /// Adds the <see cref="VersionModel"/> using <paramref name="pingOptions"/> as options.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="pingOptions"></param>
    public static IServiceCollection AddPing(this IServiceCollection services, PingOptions pingOptions)
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