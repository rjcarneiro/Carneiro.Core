using Microsoft.Extensions.DependencyInjection;

namespace Carneiro.Core.Web.Ping;

/// <summary>
/// Extensions for ping.
/// </summary>
public static class PingExtensions
{
    /// <summary>
    /// Adds the ping controller.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddPingController(this IServiceCollection services)
    {
        services.AddSingleton(_ => VersionHelper.GetVersion());

        return services;
    }
}