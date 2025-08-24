namespace Carneiro.Core.IpChecker;

/// <inheritdoc />
public sealed class SuccessIpAddressResult : IpAddressResult
{
    /// <inheritdoc />
    public override bool IsSuccess => true;

    /// <inheritdoc />
    public SuccessIpAddressResult(string ipAddress)
    {
        IpAddress = ipAddress;
    }
}