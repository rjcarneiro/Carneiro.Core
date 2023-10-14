namespace Carneiro.Core.Health;

/// <summary>
/// The version helper.
/// </summary>
public static class VersionHelper
{
    /// <summary>
    /// Gets the version.
    /// </summary>
    public static string GetSimplerVersion() => GetVersion().Version;

    /// <summary>
    /// Gets the version.
    /// </summary>
    public static VersionModel GetVersion()
    {
        Assembly assembly = Assembly.GetEntryAssembly() 
                            ?? Assembly.GetExecutingAssembly();

        return new VersionModel
        {
            Date = File.GetLastWriteTime(assembly.Location),
            Version = assembly.GetName().Version!.ToString()
        };
    }
}