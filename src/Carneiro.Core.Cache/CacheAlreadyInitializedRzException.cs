namespace Carneiro.Core.Cache;

/// <summary>
/// 
/// </summary>
public class CacheAlreadyInitializedRzException : CacheRzException
{
    /// <summary>
    /// 
    /// </summary>
    public CacheAlreadyInitializedRzException()
        : base("Cache already initialized")
    {
    }
}