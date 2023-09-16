namespace Carneiro.Core.Web;

/// <summary>
/// Class that represents a temporary media file.
/// </summary>
public class TempMediaModel
{
    /// <summary>
    /// Gets or sets the type of the MIME.
    /// </summary>
    /// <value>
    /// The type of the MIME.
    /// </value>
    public string MimeType { get; init; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; init; }

    /// <summary>
    /// Gets or sets the path.
    /// </summary>
    /// <value>
    /// The path.
    /// </value>
    public string Path { get; init; }

    /// <summary>
    /// Gets or sets the full physical path.
    /// </summary>
    public string FullPhysicalPath { get; init; }

    /// <summary>
    /// Gets or sets the size in bytes.
    /// </summary>
    public long Size { get; init; }

    /// <inheritdoc />
    public override string ToString() => $"{nameof(MimeType)}: {MimeType}, {nameof(Name)}: {Name}, {nameof(Path)}: {Path}, {nameof(FullPhysicalPath)}: {FullPhysicalPath}, {nameof(Size)}: {Size}";
}