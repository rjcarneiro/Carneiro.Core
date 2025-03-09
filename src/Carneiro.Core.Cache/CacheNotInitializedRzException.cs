namespace Carneiro.Core.Cache;

/// <summary>
/// Exception is thrown when the cache is accessed but not initialized.
/// </summary>
public class CacheNotInitializedRzException : CacheRzException
{
    /// <inheritdoc />
    public CacheNotInitializedRzException()
        : base("Cache is not initialized")
    {
    }
}