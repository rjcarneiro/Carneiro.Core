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
    public static JobServiceBuilder AddJobService(this IServiceCollection services)
    {
        services.AddHostedService<JobOnceOffBackgroundService>();

        return new JobServiceBuilder(services);
    }
}

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
        _services.TryAddEnumerable(ServiceDescriptor.Scoped<IJob, TJob>());
        return this;
    }
}