using Carneiro.Core.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Carneiro.Core.WebApi;

/// <summary>
/// Extensions for <see cref="HttpResponse"/>.
/// </summary>
public static class HttpResponseExtensions
{
    /// <summary>
    /// Gets the content asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpResponseMessage">The HTTP response message.</param>
    /// <returns></returns>
    public static async Task<T> GetContentAsync<T>(this HttpResponseMessage httpResponseMessage) => JsonHelper.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync());

    /// <summary>
    /// Reads as asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public static async Task<T> ReadAsAsync<T>(this HttpContent content) => await JsonHelper.DeserializeAsync<T>(await content.ReadAsStringAsync());

    /// <summary>
    /// Gets the content of the header <see cref="HeaderNames.SetCookie"/>.
    /// </summary>
    /// <param name="httpResponseMessage"></param>
    /// <returns></returns>
    public static string GetAuthenticationCookieValue(this HttpResponseMessage httpResponseMessage)
    {
        if (!httpResponseMessage.Headers.Contains(HeaderNames.SetCookie))
            return null;

        return httpResponseMessage.Headers.GetValues(HeaderNames.SetCookie).Last();
    }
}