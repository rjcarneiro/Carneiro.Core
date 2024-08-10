namespace Carneiro.Core.Repository;

/// <summary>
/// Default Database Context.
/// </summary>
/// <seealso cref="IDataProtectionKeyContext" />
/// <seealso cref="DbContext" />
public abstract class RzDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RzDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    protected RzDbContext(DbContextOptions<RzDbContext> options)
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
    }
}