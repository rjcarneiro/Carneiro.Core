using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Carneiro.Core.Repository.Abstractions;

/// <summary>
/// The database builder.
/// </summary>
public interface IDatabaseBuilder
{
    /// <summary>
    /// Adds a basic <see cref="ServiceLifetime.Scoped"/> implementation of <see cref="IUnitOfWork{T}"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="DbContext"/>.</typeparam>
    IDatabaseBuilder AddUnitOfWork<T>() where T : DbContext;

    /// <summary>
    /// Adds a new <see cref="IUnitOfWork{TDbContext}"/> with <see cref="ServiceLifetime.Scoped"/> lifetime.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    /// <typeparam name="TDbContext">The <see cref="DbContext"/>.</typeparam>
    IDatabaseBuilder AddUnitOfWork<TService, TImplementation, TDbContext>()
        where TDbContext : DbContext
        where TService : class, IUnitOfWork<TDbContext>
        where TImplementation : class, TService;

    /// <summary>
    /// Adds data protection keys to database <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    IDataProtectionBuilder AddDataProtection<T>() where T : DbContext, IDataProtectionKeyContext;

    /// <summary>
    /// Adds data protection keys to database <typeparamref name="T"/> with application name <paramref name="appName"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="appName">Name of the application.</param>
    IDataProtectionBuilder AddDataProtection<T>(string appName) where T : DbContext, IDataProtectionKeyContext;

    /// <summary>
    /// Adds data protection keys to database <typeparamref name="T"/> with default key life time <paramref name="keyLifeTime"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="keyLifeTime">The key lifetime.</param>
    IDataProtectionBuilder AddDataProtection<T>(TimeSpan keyLifeTime) where T : DbContext, IDataProtectionKeyContext;

    /// <summary>
    /// Adds data protection keys to database <typeparamref name="T"/> with application name <paramref name="appName"/> and default key life time <paramref name="keyLifeTime"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="appName">Name of the application.</param>
    /// <param name="keyLifeTime">The key lie time.</param>
    IDataProtectionBuilder AddDataProtection<T>(string appName, TimeSpan keyLifeTime) where T : DbContext, IDataProtectionKeyContext;
}