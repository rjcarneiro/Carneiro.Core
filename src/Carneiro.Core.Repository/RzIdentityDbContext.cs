using Carneiro.Core.Repository.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Carneiro.Core.Repository;

/// <summary>
/// Default Identity database context.
/// </summary>
/// <typeparam name="TUser">The type of the user.</typeparam>
/// <typeparam name="TRole">The type of the role.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <seealso cref="IdentityDbContext{TUser, TRole, TKey}" />
/// <seealso cref="IDataProtectionKeyContext" />
public abstract class RzIdentityDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey>, IDataProtectionKeyContext
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// A collection of <see cref="T:Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey" />
    /// </summary>
    public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RzIdentityDbContext{TUser, TRole, TKey}" /> class.
    /// </summary>
    /// <param name="dbContextOptions">The database context options.</param>
    protected RzIdentityDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }

    /// <summary>
    /// Configures the schema needed for the identity framework.
    /// </summary>
    /// <param name="builder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseUtcDateTimeRead();

        builder.ApplyConfiguration(new DataProtectionKeyConfiguration());
    }
}

/// <summary>
/// Default Identity Db Context.
/// </summary>
/// <typeparam name="TUser">The type of the user.</typeparam>
/// <typeparam name="TRole">The type of the role.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TUserClaim">The type of the user claim.</typeparam>
/// <typeparam name="TUserRole">The type of the user role.</typeparam>
/// <typeparam name="TUserLogin">The type of the user login.</typeparam>
/// <typeparam name="TRoleClaim">The type of the role claim.</typeparam>
/// <typeparam name="TUserToken">The type of the user token.</typeparam>
/// <seealso cref="IDataProtectionKeyContext" />
/// <seealso cref="IdentityDbContext{TUser, TRole, TKey}" />
public abstract class RzIdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim,
    TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>, IDataProtectionKeyContext
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
    where TUserClaim : IdentityUserClaim<TKey>
    where TUserRole : IdentityUserRole<TKey>
    where TUserLogin : IdentityUserLogin<TKey>
    where TRoleClaim : IdentityRoleClaim<TKey>
    where TUserToken : IdentityUserToken<TKey>
{
    /// <summary>
    /// A collection of <see cref="T:Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey" />
    /// </summary>
    public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RzIdentityDbContext{TUser, TRole, TKey}" /> class.
    /// </summary>
    /// <param name="dbContextOptions">The database context options.</param>
    protected RzIdentityDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }

    /// <summary>
    /// Configures the schema needed for the identity framework.
    /// </summary>
    /// <param name="builder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseUtcDateTimeRead();

        builder.ApplyConfiguration(new DataProtectionKeyConfiguration());
    }
}