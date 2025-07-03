using System.Diagnostics.CodeAnalysis;

namespace Carneiro.Core.Host;

/// <summary>
/// Extensions for <see cref="IJob"/>.
/// </summary>
public static class JobExtensions
{
    /// <summary>
    /// Adds a new <see cref="IJob"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TJob"></typeparam>
    public static IServiceCollection AddJob<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TJob>(this IServiceCollection services)
        where TJob : class, IJob
    {
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IJob, TJob>());
        return services;
    }
}