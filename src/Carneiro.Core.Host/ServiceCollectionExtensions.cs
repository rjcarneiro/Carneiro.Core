using System.Diagnostics.CodeAnalysis;

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

    /// <summary>
    /// Adds a new <see cref="IJob"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TJob"></typeparam>
    public static IServiceCollection AddJob<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TJob>(this IServiceCollection services)
        where TJob : class, IJob
    {
        services.AddSingleton<TJob>();
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IJob, TJob>(sp => sp.GetRequiredService<TJob>()));
        return services;
    }
}