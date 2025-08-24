namespace Carneiro.Core.IpChecker;

/// <summary>
/// The Ip Address checker from <c>ifconfig.me</c>.
/// </summary>
/// <param name="logger"></param>
/// <param name="httpClient"></param>
public class IfConfigIpAddressHttpClient(ILogger<IfConfigIpAddressHttpClient> logger, HttpClient httpClient) : IIpAddressHttpClient
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IpAddressResult> GetIpAddress()
    {
        try
        {
            var ipAddress = await httpClient.GetStringAsync("ip");

            return new SuccessIpAddressResult(ipAddress);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to get remote ip address from ifconfig.me");
        }

        return new FailedIpAddressResult();
    }
}