namespace Carneiro.Core.Exceptions;

/// <summary>
/// Default Exception.
/// </summary>
/// <seealso cref="System.Exception" />
public class RzException : Exception
{
    private const string DefaultMessage = "An unknown exception happened";

    /// <summary>
    /// Initializes a new instance of the <see cref="RzException"/> class.
    /// </summary>
    public RzException()
        : base(DefaultMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RzException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public RzException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RzException"/> class.
    /// </summary>
    /// <param name="innerException">The inner exception.</param>
    public RzException(Exception innerException)
        : base(DefaultMessage, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RzException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public RzException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}