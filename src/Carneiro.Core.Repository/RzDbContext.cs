using Carneiro.Core.Repository.Configurations;
using Carneiro.Core.Repository.Extensions;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Carneiro.Core.Repository;

/// <summary>
/// Default Database Context.
/// </summary>
/// <seealso cref="IDataProtectionKeyContext" />
/// <seealso cref="DbContext" />
public abstract class RzDbContext : DbContext, IDataProtectionKeyContext
{
    /// <summary>
    /// A collection of <see cref="T:Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey" />
    /// </summary>
    public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RzDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    protected RzDbContext(DbContextOptions<RzDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RzDbContext" /> class.
    /// </summary>
    /// <param name="options">The options.</param>
    protected RzDbContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <summary>
    /// Configures the schema needed for the identity framework.
    /// </summary>
    /// <param name="builder">The builder.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseUtcDateTimeRead();

        builder.ApplyConfiguration(new DataProtectionKeyConfiguration());
    }
}