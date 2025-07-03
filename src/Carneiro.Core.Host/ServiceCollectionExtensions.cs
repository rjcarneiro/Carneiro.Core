namespace Carneiro.Core.Host;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds <see cref="JobOnceOffBackgroundService"/> as <see cref="IHostedService"/>.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddJobService(this IServiceCollection services) => services.AddHostedService<JobOnceOffBackgroundService>();
}