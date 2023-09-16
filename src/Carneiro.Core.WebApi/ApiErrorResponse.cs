using Carneiro.Core.WebApi.Abstractions;

namespace Carneiro.Core.WebApi;

/// <summary>
/// Api error response.
/// </summary>
/// <seealso cref="IApiErrorResponse" />
public class ApiErrorResponse : IApiErrorResponse
{
    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    public string Message { get; init; }

    /// <summary>
    /// Defaults the specified message.
    /// </summary>
    /// <returns></returns>
    public static ApiErrorResponse Default() => new() { Message = "An unknown error happened." };
}

/// <summary>
/// Api error response with <typeparamref name="T"/> as data.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiErrorResponse<T> : ApiErrorResponse, IApiErrorResponse<T> where T : class
{
    /// <summary>
    /// Gets the data.
    /// </summary>
    public T Data { get; init; }
}