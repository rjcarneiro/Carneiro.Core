using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Carneiro.Core.Host;

/// <summary>
/// Extensions for <see cref="IWorker"/>.
/// </summary>
public static class WorkerExtensions
{
    /// <summary>
    /// Adds a new <see cref="IWorker"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="THostedService"></typeparam>
    public static IServiceCollection AddWorker<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THostedService>(this IServiceCollection services)
        where THostedService : class, IWorker
    {
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IWorker, THostedService>());
        return services;
    }
}