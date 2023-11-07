namespace Carneiro.Core.Web;

/// <summary>
/// Class that defines the ping options.
/// </summary>
public class PingOptions
{
    /// <summary>
    /// Gets or sets the default route where the ping endpoint will live. Default is <c>/api/ping</c>.
    /// </summary>
    public string Route { get; set; } = "/api/ping";
    
    /// <summary>
    /// Flag that enables returning a <see cref="HttpStatusCode.OK"/> with <see cref="VersionModel"/> as body. Otherwise returns an empty <see cref="HttpStatusCode.NoContent"/>.
    /// </summary>
    public bool ShowVersion { get; set; } = true;

    /// <inheritdoc />
    public override string ToString() => $"{nameof(Route)}: {Route}, {nameof(ShowVersion)}: {ShowVersion}";
}