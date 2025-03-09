namespace Carneiro.Core.Cache;

/// <summary>
/// 
/// </summary>
public class CacheNotInitializedRzException : CacheRzException
{
    /// <inheritdoc />
    public CacheNotInitializedRzException()
        : base("Cache is not initialized")
    {
    }
}