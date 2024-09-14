using Carneiro.Core.Web;

namespace Carneiro.Core.Tests.Extensions;

/// <summary>
/// Web extensions for <see cref="BaseScenario"/>.
/// </summary>
public static class BaseScenarioExtensions
{
    /// <summary>
    /// Converts a <see cref="IFormFile"/> into a <see cref="TempMediaModel"/>.
    /// </summary>
    /// <param name="baseScenario"></param>
    /// <param name="file"></param>
    public static TempMediaModel ConvertToTempMediaModel(this BaseScenario baseScenario, IFormFile file) => new()
    {
        FullPhysicalPath = $"{UploadConstants.WwwrootPath}/Temporary/{file.FileName}",
        MimeType = file.ContentType,
        Name = file.Name,
        Path = $"Temporary/{file.FileName}",
        Size = file.Length
    };

    /// <summary>
    /// Converts a <see cref="IFormFile"/> into a <see cref="TempMediaModel"/>.
    /// </summary>
    /// <param name="scenario"></param>
    /// <param name="file"></param>
    public static Mock<IFormFile> ConvertToIFormFileMock(this BaseScenario scenario, TempMediaModel file)
    {
        var fileMock = new Mock<IFormFile>();

        fileMock.Setup(t => t.OpenReadStream()).Returns(() => new FileStream(file.FullPhysicalPath, FileMode.Open, FileAccess.Read));
        fileMock.Setup(t => t.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns(async (Stream s, CancellationToken t) =>
        {
            await using Stream fileStream = fileMock.Object.OpenReadStream();
            await fileStream.CopyToAsync(s, t);
        });
        fileMock.Setup(_ => _.Name).Returns(file.Name);
        fileMock.Setup(_ => _.FileName).Returns(file.Name);
        fileMock.Setup(_ => _.Length).Returns(file.Size);
        fileMock.Setup(_ => _.ContentType).Returns(file.MimeType);

        return fileMock;
    }
}