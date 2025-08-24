namespace Carneiro.Core.IpChecker;

/// <summary>
/// The Ip address cache item.
/// </summary>
/// <param name="IpAddress"></param>
/// <param name="Date"></param>
public record IpAddressCheckerCacheItem(string IpAddress, DateTime Date);