namespace Carneiro.Core.IpChecker;

/// <summary>
/// The <see cref="IIpAddressChecker"/> options.
/// </summary>
public class IpAddressCheckerOptions
{
    /// <summary>
    /// Gets or sets a flag to persist the latest valid Ip address.
    /// </summary>
    public bool UsePersistenceCache { get; set; }

    /// <summary>
    /// Gets or sets a timeout for the latest persisted Ip address.
    /// </summary>
    public TimeSpan? PersistenceCacheTimeout { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(UsePersistenceCache)}: {UsePersistenceCache}, {nameof(PersistenceCacheTimeout)}: {PersistenceCacheTimeout}";
    }
}