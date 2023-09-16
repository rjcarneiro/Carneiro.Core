using Carneiro.Core.Utils.Abstractions;

namespace Carneiro.Core.Utils;

/// <summary>
/// The file utility implementation.
/// </summary>
public sealed class FileUtil : IFileUtil
{
    private readonly ILogger<FileUtil> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileUtil"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public FileUtil(ILogger<FileUtil> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public void Move(string origin, string destination) => File.Move(origin, destination);

    /// <inheritdoc/>
    public void Delete(string path)
    {
        try
        {
            File.Delete(path);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to delete file in \'{Path}\'", path);
        }
    }

    /// <inheritdoc/>
    public bool Exists(string fullPhysicalPath) => File.Exists(fullPhysicalPath);
}