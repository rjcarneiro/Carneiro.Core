using Microsoft.AspNetCore.DataProtection;

namespace Carneiro.Core.Repository;

/// <inheritdoc />
public class DatabaseBuilder : IDatabaseBuilder
{
    private readonly DatabaseOptions _databaseOptions;
    private readonly IServiceCollection _services;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseBuilder"/> class.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="databaseOptions"></param>
    public DatabaseBuilder(IServiceCollection services, DatabaseOptions databaseOptions)
    {
        _databaseOptions = databaseOptions;
        _services = services;
    }

    /// <inheritdoc />
    public virtual IDatabaseBuilder AddUnitOfWork<T>() where T : DbContext
    {
        _services.Add(new ServiceDescriptor(typeof(IUnitOfWork<T>), typeof(UnitOfWork<T>), ServiceLifetime.Scoped));
        _services.Add(new ServiceDescriptor(typeof(IStoredProcedureUnitOfWork<T>), typeof(StoredProcedureUnitOfWork<T>), ServiceLifetime.Scoped));
        _services.Add(new ServiceDescriptor(typeof(ITransactionalUnitOfWork<T>), typeof(TransactionalUnitOfWork<T>), ServiceLifetime.Scoped));

        return this;
    }

    /// <inheritdoc />
    public virtual IDatabaseBuilder AddUnitOfWork<TService, TImplementation, TDbContext>()
        where TDbContext : DbContext
        where TService : class, IUnitOfWork<TDbContext>
        where TImplementation : class, TService
    {
        _services.AddScoped<TService, TImplementation>();
        _services.AddScoped<ITransactionalUnitOfWork<TDbContext>, TransactionalUnitOfWork<TDbContext>>();
        _services.AddScoped<IStoredProcedureUnitOfWork<TDbContext>, StoredProcedureUnitOfWork<TDbContext>>();

        return this;
    }

    /// <inheritdoc />
    public virtual IDataProtectionBuilder AddDataProtection<T>() where T : DbContext, IDataProtectionKeyContext
    {
        return _services.AddDataProtection()
            .PersistKeysToDbContext<T>();
    }

    /// <inheritdoc />
    public virtual IDataProtectionBuilder AddDataProtection<T>(string appName) where T : DbContext, IDataProtectionKeyContext
    {
        return _services.AddDataProtection()
            .PersistKeysToDbContext<T>()
            .SetApplicationName(appName);
    }

    /// <inheritdoc />
    public virtual IDataProtectionBuilder AddDataProtection<T>(TimeSpan keyLifeTime) where T : DbContext, IDataProtectionKeyContext
    {
        return _services.AddDataProtection()
            .PersistKeysToDbContext<T>()
            .SetDefaultKeyLifetime(keyLifeTime);
    }

    /// <inheritdoc />
    public virtual IDataProtectionBuilder AddDataProtection<T>(string appName, TimeSpan keyLifeTime) where T : DbContext, IDataProtectionKeyContext
    {
        return _services.AddDataProtection()
            .PersistKeysToDbContext<T>()
            .SetApplicationName(appName)
            .SetDefaultKeyLifetime(keyLifeTime);
    }
}