namespace Carneiro.Core.Health;

/// <summary>
/// The version helper.
/// </summary>
public static class VersionHelper
{
    /// <summary>
    /// Gets the version.
    /// </summary>
    /// <returns></returns>
    public static string GetSimplerVersion() => GetVersion().Version;

    /// <summary>
    /// Gets the version.
    /// </summary>
    /// <returns></returns>
    public static VersionModel GetVersion()
    {
        var assembly = Assembly.GetEntryAssembly();

        return new VersionModel
        {
            Date = File.GetLastWriteTime(assembly!.Location),
            Version = assembly.GetName().Version!.ToString()
        };
    }
}