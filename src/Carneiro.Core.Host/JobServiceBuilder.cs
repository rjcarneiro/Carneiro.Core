using System.Diagnostics.CodeAnalysis;

namespace Carneiro.Core.Host;

/// <summary>
/// The builder for <see cref="JobOnceOffBackgroundService"/>.
/// </summary>
public class JobServiceBuilder
{
    private readonly IServiceCollection _services;

    internal JobServiceBuilder(IServiceCollection services)
    {
        _services = services;
    }

    /// <summary>
    /// Adds a new <see cref="IJob"/>.
    /// </summary>
    /// <typeparam name="TJob"></typeparam>
    public JobServiceBuilder AddJob<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TJob>()
        where TJob : class, IJob
    {
        _services.TryAddEnumerable(ServiceDescriptor.Singleton<IJob, TJob>());
        return this;
    }
}