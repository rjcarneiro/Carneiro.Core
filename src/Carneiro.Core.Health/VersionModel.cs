namespace Carneiro.Core.Health;

/// <summary>
/// Version View Model.
/// </summary>
public class VersionModel
{
    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    public string Version { get; init; }

    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    public DateTime Date { get; init; }

    /// <inheritdoc />
    public override string ToString() => $"{nameof(Version)}: {Version}, {nameof(Date)}: {Date}";
}