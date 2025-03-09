namespace Carneiro.Core.Cache;

/// <summary>
/// The base cache exception.
/// </summary>
public abstract class CacheRzException : RzException
{
    /// <inheritdoc />
    protected CacheRzException(string message)
        : base(message)
    {
    }
}