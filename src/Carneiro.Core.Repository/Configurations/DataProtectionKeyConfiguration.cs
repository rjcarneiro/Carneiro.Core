using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carneiro.Core.Repository.Configurations;

/// <summary>
/// Class that handles 
/// </summary>
public class DataProtectionKeyConfiguration : IEntityTypeConfiguration<DataProtectionKey>
{
    private readonly Action<EntityTypeBuilder<DataProtectionKey>> _action;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataProtectionKeyConfiguration"/> class.
    /// </summary>
    public DataProtectionKeyConfiguration()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataProtectionKeyConfiguration"/> class.
    /// </summary>
    /// <param name="action"></param>
    public DataProtectionKeyConfiguration(Action<EntityTypeBuilder<DataProtectionKey>> action)
    {
        _action = action;
    }

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<DataProtectionKey> builder)
    {
        builder.HasKey(t => t.Id);
        builder.ToTable("DataProtectionKey");
        
        _action?.Invoke(builder);
    }
}