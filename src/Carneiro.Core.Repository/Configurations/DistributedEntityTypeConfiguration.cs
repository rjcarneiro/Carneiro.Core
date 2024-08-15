using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carneiro.Core.Repository.Configurations;

/// <summary>
/// Default configuration of <see cref="IEntityDistributed"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{T}" />
public abstract class DistributedEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntityDistributed
{
    /// <summary>
    /// Configures the entity of type <typeparamref name="T" />.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(t => t.Id);

        ConfigureEntity(builder);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityTypeConfiguration{T}"/> class.
    /// </summary>
    /// <param name="builder">The builder.</param>
    protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}