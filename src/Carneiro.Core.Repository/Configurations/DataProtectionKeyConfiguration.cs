using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carneiro.Core.Repository.Configurations;

internal class DataProtectionKeyConfiguration : IEntityTypeConfiguration<DataProtectionKey>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<DataProtectionKey> builder)
    {
        builder.HasKey(t => t.Id);
        builder.ToTable("DataProtectionKey");
    }
}