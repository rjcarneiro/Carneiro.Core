using Carneiro.Core.Repository.Configurations;

namespace Carneiro.Core.Repository;

/// <summary>
/// Database implementation with <see cref="IDataProtectionKeyContext"/>.
/// </summary>
public abstract class RzDataProtectionDbContext : RzDbContext, IDataProtectionKeyContext
{
    /// <summary>
    /// A collection of <see cref="T:Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey" />
    /// </summary>
    public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RzDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    protected RzDataProtectionDbContext(DbContextOptions<RzDataProtectionDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RzDbContext" /> class.
    /// </summary>
    /// <param name="options">The options.</param>
    protected RzDataProtectionDbContext(DbContextOptions options)
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
        builder.ApplyConfiguration(new DataProtectionKeyConfiguration());
    }
}