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
        services.Add(new ServiceDescriptor(typeof(ITransactionalUnitOfWork), typeof(TransactionalUnitOfWork<T>), ServiceLifetime.Scoped));

        return services;
    }

    /// <summary>
    /// Adds a new <see cref="IUnitOfWork"/> with <see cref="ServiceLifetime.Scoped"/> lifetime.
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWork<TService, TImplementation, TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
        where TService : class, IUnitOfWork
        where TImplementation : UnitOfWork<TDbContext>, TService
    {
        services.AddScoped<TService, TImplementation>();
        services.AddScoped<ITransactionalUnitOfWork, TransactionalUnitOfWork<TDbContext>>();

        return services;
    }
}