namespace Carneiro.Core.IpChecker;

/// <summary>
/// The Ip Address Http Client abstraction.
/// </summary>
internal interface IIpAddressHttpClient
{
    /// <summary>
    /// Gets the Ip address from an external provider.
    /// </summary>
    Task<IpAddressResult> GetIpAddress();
}