namespace Carneiro.Core.Utils.Abstractions;

/// <summary>
/// The file utility.
/// </summary>
public interface IFileUtil
{
    /// <summary>
    /// Moves a file from <paramref name="origin"/> into <paramref name="destination"/>.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="destination"></param>
    void Move(string origin, string destination);

    /// <summary>
    /// Deletes a file in <paramref name="path"/>.
    /// </summary>
    /// <param name="path"></param>
    void Delete(string path);

    /// <summary>
    /// Checks if a certain <paramref name="fullPhysicalPath"/> exists.
    /// </summary>
    /// <param name="fullPhysicalPath"></param>
    /// <returns></returns>
    bool Exists(string fullPhysicalPath);
}