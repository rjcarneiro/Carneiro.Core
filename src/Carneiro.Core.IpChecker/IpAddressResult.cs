namespace Carneiro.Core.IpChecker;

/// <summary>
/// The Ip address result
/// </summary>
public abstract class IpAddressResult
{
    /// <summary>
    /// Gets the result of the query.
    /// </summary>
    public virtual bool IsSuccess { get; init; }

    /// <summary>
    /// Gets the Ip Address.
    /// </summary>
    public virtual string IpAddress { get; protected init; }
}