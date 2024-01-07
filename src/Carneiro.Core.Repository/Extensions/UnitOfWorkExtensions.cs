namespace Carneiro.Core.Repository.Extensions;

/// <summary>
/// Extensions for <see cref="IUnitOfWork"/>.
/// </summary>
public static class UnitOfWorkExtensions
{
    /// <summary>
    /// Adds a basic <see cref="ServiceLifetime.Scoped"/> implementation of <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <param name="services">The services.</param>
    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : DbContext
    {
        services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork<T>), ServiceLifetime.Scoped));

        return services;
    }

    /// <summary>
    /// Adds a new <see cref="IUnitOfWork"/> with <see cref="ServiceLifetime.Scoped"/> lifetime.
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWork<TService, TImplementation>(this IServiceCollection services)
        where TService : class, IUnitOfWork
        where TImplementation : UnitOfWork<DbContext>, TService
    {
        services.AddScoped<TService, TImplementation>();

        return services;
    }
}