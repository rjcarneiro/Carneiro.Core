namespace Carneiro.Core.Cache;

/// <summary>
/// The entity cache options class.
/// </summary>
public abstract class EntityCacheOptions
{
    /// <summary>
    /// Gets the default section name for cache.
    /// </summary>
    public const string DefaultSectionName = "Cache";

    /// <summary>
    /// Gets or sets the cache refresh duration.
    /// </summary>
    public TimeSpan CacheDuration { get; set; } = TimeSpan.FromMinutes(5);
}