namespace Carneiro.Core.Exceptions;

/// <summary>
/// Http Connectivity Exception class.
/// </summary>
/// <seealso cref="Carneiro.Core.Exceptions.RzException" />
public class HttpConnectivityRzException : RzException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HttpConnectivityRzException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public HttpConnectivityRzException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpConnectivityRzException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public HttpConnectivityRzException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpConnectivityRzException"/> class.
    /// </summary>
    /// <param name="innerException">The inner exception.</param>
    public HttpConnectivityRzException(Exception innerException)
        : base("There was a connectivity problem while doing an http request", innerException)
    {
    }
}