namespace Carneiro.Core.Web.Abstractions;

/// <summary>
/// Default Api Error Response.
/// </summary>
public interface IApiErrorResponse
{
    /// <summary>
    /// Gets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    string Message { get; }
}

/// <summary>
/// Api error response with <typeparamref name="T"/> as data.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IApiErrorResponse<out T> : IApiErrorResponse where T : class
{
    /// <summary>
    /// Gets the data.
    /// </summary>
    T Data { get; }
}