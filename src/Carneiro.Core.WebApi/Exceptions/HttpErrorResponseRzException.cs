namespace Carneiro.Core.WebApi.Exceptions;

/// <summary>
/// Exceptions happens when an error response is sent from the server.
/// </summary>
public class HttpErrorResponseRzException : RzException
{
    /// <summary>
    /// Gets the <see cref="HttpStatusCode"/>.
    /// </summary>
    public HttpStatusCode Status { get;  }
    
    /// <summary>
    /// Gets the http response body.
    /// </summary>
    public string Body { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpErrorResponseRzException" /> class.
    /// </summary>
    /// <param name="httpStatusCode"></param>
    /// <param name="body"></param>
    public HttpErrorResponseRzException(HttpStatusCode httpStatusCode, string body = null)
    {
        Status = httpStatusCode;
        Body = body;
    }
}