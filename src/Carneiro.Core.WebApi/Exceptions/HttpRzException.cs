using System.Net;
using Carneiro.Core.Exceptions;
using Carneiro.Core.WebApi.Abstractions;

namespace Carneiro.Core.WebApi.Exceptions;

/// <summary>
/// The exception happens when the <see cref="IHttpOrchestrator"/> detects some error http response.
/// </summary>
/// <seealso cref="RzException" />
public class HttpRzException : RzException
{
    private const string DefaultMessage = "Error response from http request";

    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    /// <value>
    /// The HTTP status code.
    /// </value>
    public HttpStatusCode HttpStatusCode { get; }

    /// <summary>
    /// Gets the API error response.
    /// </summary>
    /// <value>
    /// The API error response.
    /// </value>
    public IApiErrorResponse ApiErrorResponse { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpRzException"/> class.
    /// </summary>
    /// <param name="innerException">The inner exception.</param>
    public HttpRzException(Exception innerException)
        : base(DefaultMessage, innerException)
    {
        HttpStatusCode = HttpStatusCode.InternalServerError;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpRzException"/> class.
    /// </summary>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    public HttpRzException(HttpStatusCode httpStatusCode)
    {
        HttpStatusCode = httpStatusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpRzException"/> class.
    /// </summary>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    /// <param name="apiErrorResponse">The API error response.</param>
    public HttpRzException(HttpStatusCode httpStatusCode, IApiErrorResponse apiErrorResponse)
    {
        HttpStatusCode = httpStatusCode;
        ApiErrorResponse = apiErrorResponse;
    }
}