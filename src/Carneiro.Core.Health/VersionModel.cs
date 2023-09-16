namespace Carneiro.Core.Health;

/// <summary>
/// Version View Model.
/// </summary>
public class VersionModel
{
    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    /// <value>
    /// The version.
    /// </value>
    public string Version { get; init; }

    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    /// <value>
    /// The date.
    /// </value>
    public DateTime Date { get; init; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() => $"{nameof(Version)}: {Version}, {nameof(Date)}: {Date}";
}