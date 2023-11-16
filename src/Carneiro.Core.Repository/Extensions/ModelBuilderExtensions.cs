using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Carneiro.Core.Repository.Extensions;

/// <summary>
/// <see cref="ModelBuilder"/> Extensions.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Register all <see cref="DateTime"/> to read as <see cref="DateTimeKind.Utc"/> from Sql.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void UseUtcDateTimeRead(this ModelBuilder modelBuilder)
    {
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        foreach (IMutableProperty property in entityType.GetProperties())
            if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                property.SetValueConverter(dateTimeConverter);
    }
}