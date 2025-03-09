namespace Carneiro.Core.Cache;

/// <summary>
/// Exception is thrown when the cache tries to be initialized more than once.
/// </summary>
public class CacheAlreadyInitializedRzException : CacheRzException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CacheAlreadyInitializedRzException"/> class.
    /// </summary>
    public CacheAlreadyInitializedRzException()
        : base("Cache already initialized")
    {
    }
}