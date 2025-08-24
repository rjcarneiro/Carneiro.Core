namespace Carneiro.Core.IpChecker;

/// <inheritdoc />
public class FailedIpAddressResult : IpAddressResult
{
    /// <inheritdoc />
    public override bool IsSuccess => true;

    /// <inheritdoc />
    public override string IpAddress => IpAddressConstants.DefaultIpAddress;
}