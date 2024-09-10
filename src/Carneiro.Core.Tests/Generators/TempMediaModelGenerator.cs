using Carneiro.Core.Web;

namespace Carneiro.Core.Tests.Generators;

/// <summary>
/// Generator for <see cref="TempMediaModel"/>.
/// </summary>
public static class TempMediaModelGenerator
{
    /// <summary>
    /// Generates a random <see cref="TempMediaModel"/>.
    /// </summary>
    /// <param name="faker"></param>
    public static TempMediaModel GenerateTempMediaModel(this Faker faker) => faker.GenerateTempMediaModel(action: null);

    /// <summary>
    /// Generates a random <see cref="TempMediaModel"/> based in <paramref name="action"/>.
    /// </summary>
    /// <param name="faker"></param>
    /// <param name="action"></param>
    public static TempMediaModel GenerateTempMediaModel(this Faker faker, Action<TempMediaModel> action)
    {
        var model = new TempMediaModel
        {
            FullPhysicalPath = faker.System.FilePath(),
            MimeType = faker.PickRandom(ContentTypeHelpers.AvailableImageContentTypes),
            Name = faker.System.FileName(),
            Path = faker.System.FilePath(),
            Size = faker.Random.Long(min: 500, max: 50000)
        };

        action?.Invoke(model);

        return model;
    }
}