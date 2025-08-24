namespace Carneiro.Core.IpChecker;

/// <summary>
/// The Ip Address checker from <c>ipify.org</c>.
/// </summary>
/// <param name="logger"></param>
/// <param name="httpClient"></param>
public class IpIfyIpAddressHttpClient(ILogger<IpIfyIpAddressHttpClient> logger, HttpClient httpClient) : IIpAddressHttpClient
{
    /// <inheritdoc />
    public async Task<IpAddressResult> GetIpAddress()
    {
        try
        {
            var httpResponse = await httpClient.GetAsync(string.Empty);

            return new SuccessIpAddressResult(await httpResponse.Content.ReadAsStringAsync());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to get remote ip address from api.ipify.org");
        }

        return new FailedIpAddressResult();
    }
}