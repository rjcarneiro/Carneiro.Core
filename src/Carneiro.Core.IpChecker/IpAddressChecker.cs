using Carneiro.Core.Json;

namespace Carneiro.Core.IpChecker;

/// <inheritdoc />
public class IpAddressChecker(ILogger<IpAddressChecker> logger, IOptions<IpAddressCheckerOptions> options, IEnumerable<IIpAddressHttpClient> ipAddressHttpClients) : IIpAddressChecker
{
    private const string CacheFile = "ip_checker.cache";

    private readonly IpAddressCheckerOptions _options = options.Value;

    /// <inheritdoc />
    public async Task<string> GetRemoteIpAddress()
    {
        if (_options.UsePersistenceCache && File.Exists(CacheFile))
        {
            var cacheItem = JsonHelper.Deserialize<IpAddressCheckerCacheItem>(await File.ReadAllTextAsync(CacheFile));
            if (cacheItem is not null && !cacheItem.IsCacheExpired(_options))
            {
                return cacheItem.IpAddress;
            }
        }

        foreach (var ipChecker in ipAddressHttpClients)
        {
            var result = await ipChecker.GetIpAddress();
            if (!result.IsSuccess)
            {
                continue;
            }

            if (!_options.UsePersistenceCache)
            {
                return result.IpAddress;
            }

            try
            {
                logger.LogInformation("Updated cache file '{File}' with the latest ip '{IpAddress}'", CacheFile, result.IpAddress);
                await File.WriteAllTextAsync(CacheFile, JsonHelper.Serialize(new IpAddressCheckerCacheItem(result.IpAddress, DateTime.UtcNow)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to update cache file '{File}'", CacheFile);
            }

            return result.IpAddress;
        }

        logger.LogWarning("Unable to find remote ip address");
        return IpAddressConstants.DefaultIpAddress;
    }
}