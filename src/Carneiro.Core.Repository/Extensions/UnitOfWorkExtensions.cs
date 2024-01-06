namespace Carneiro.Core.Repository.Extensions;

/// <summary>
/// Extensions for <see cref="IUnitOfWork"/>.
/// </summary>
public static class UnitOfWorkExtensions
{
    /// <summary>
    /// Registers a basic <see cref="ServiceLifetime.Scoped"/> implementation of <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <param name="services">The services.</param>
    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : DbContext
    {
        services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork<T>), ServiceLifetime.Scoped));
        
        return services;
    }
}