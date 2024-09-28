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
        Assembly assembly = GetAssembly();

        return assembly.GetName().Version!.ToString();
    }

    /// <summary>
    /// Gets the version.
    /// </summary>
    public static VersionModel GetVersion()
    {
        Assembly assembly = GetAssembly();

        return new VersionModel
        {
            Date = File.GetLastWriteTime(string.IsNullOrEmpty(assembly.Location) ? AppContext.BaseDirectory : assembly.Location).ToUniversalTime(), 
            Version = assembly.GetName().Version!.ToString(),
        };
    }

    private static Assembly GetAssembly() => Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
}