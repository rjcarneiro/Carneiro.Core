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
    public string Message { get; init; }

    /// <summary>
    /// Defaults the specified message.
    /// </summary>
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