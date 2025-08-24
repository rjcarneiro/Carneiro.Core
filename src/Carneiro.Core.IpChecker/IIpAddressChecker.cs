namespace Carneiro.Core.IpChecker;

/// <summary>
/// The Ip Address Checker interface
/// </summary>
public interface IIpAddressChecker
{
    /// <summary>
    /// Gets the remote Ip address.
    /// </summary>
    Task<string> GetRemoteIpAddress();
}