namespace Carneiro.Core.Health;

/// <summary>
/// The version helper.
/// </summary>
public static class VersionHelper
{
    /// <summary>
    /// Gets the version.
    /// </summary>
    public static string GetSimplerVersion()
    {
        Assembly assembly = Assembly.GetEntryAssembly() 
                            ?? Assembly.GetExecutingAssembly();

        return assembly.GetName().Version!.ToString();
    }

    /// <summary>
    /// Gets the version.
    /// </summary>
    public static VersionModel GetVersion()
    {
        Assembly assembly = Assembly.GetEntryAssembly() 
                            ?? Assembly.GetExecutingAssembly();

        return new VersionModel
        {
            Date = File.GetLastWriteTime(string.IsNullOrEmpty(assembly.Location) ? AppContext.BaseDirectory : assembly.Location),
            Version = assembly.GetName().Version!.ToString()
        };
    }
}