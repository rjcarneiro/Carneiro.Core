namespace Carneiro.Core.IpChecker;

/// <summary>
/// Extensions for <see cref="IpAddressCheckerCacheItem"/>.
/// </summary>
public static class IpAddressCheckerCacheItemExtensions
{
    /// <summary>
    /// Checks either the <see cref="IpAddressCheckerCacheItem"/> has the ip cached expired.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="options"></param>
    public static bool IsCacheExpired(this IpAddressCheckerCacheItem item, IpAddressCheckerOptions options) => item.Date.Add(options.PersistenceCacheTimeout ?? TimeSpan.FromHours(1)) < DateTime.UtcNow;
}