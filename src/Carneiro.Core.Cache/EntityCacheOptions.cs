namespace Carneiro.Core.Cache;

/// <summary>
/// 
/// </summary>
public abstract class EntityCacheOptions
{
    /// <summary>
    /// 
    /// </summary>
    public const string DefaultSectionName = "Cache";

    /// <summary>
    /// 
    /// </summary>
    public TimeSpan CacheDuration { get; set; } = TimeSpan.FromMinutes(5);
}