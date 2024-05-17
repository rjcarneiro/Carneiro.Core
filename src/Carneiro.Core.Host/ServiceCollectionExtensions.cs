namespace Carneiro.Core.Host;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds <see cref="WorkerService"/> as <see cref="IHostedService"/>.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddWorkerService(this IServiceCollection services) => services.AddHostedService<WorkerService>();
}