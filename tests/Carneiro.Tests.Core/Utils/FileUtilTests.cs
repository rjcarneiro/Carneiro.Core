namespace Carneiro.Tests.Core.Utils;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class FileUtilTests
{
    private IFileUtil _fileUtil;

    [SetUp]
    public void Setup() => _fileUtil = new FileUtil(new Mock<ILogger<FileUtil>>().Object);

    [Test]
    public void Move_FileExists_ShouldMove()
    {
        const string FileName = "test.txt";
        const string NewDirectory = "move";

        // Arrange
        if (!File.Exists(FileName))
        {
            using FileStream fileStream = File.Create(FileName);
        }

        if (!Directory.Exists(NewDirectory))
            Directory.CreateDirectory(NewDirectory);

        // Act
        _fileUtil.Move(FileName, $"{NewDirectory}/{FileName}");

        // Assert
        Assert.That(File.Exists(FileName), Is.False);
        Assert.That(File.Exists($"{NewDirectory}/{FileName}"), Is.True);

        // clean up
        File.Delete($"{NewDirectory}/{FileName}");
    }
}