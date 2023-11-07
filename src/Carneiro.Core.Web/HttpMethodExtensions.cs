namespace Carneiro.Core.Web;

/// <summary>
/// Extensions for <see cref="HttpMethod"/>
/// </summary>
public static class HttpMethodExtensions
{
    /// <summary>
    /// Flag that makes sure the http request has body or not.
    /// </summary>
    /// <param name="httpMethod"></param>
    /// <param name="input"></param>
    /// <typeparam name="TInput"></typeparam>
    public static bool AllowsBody<TInput>(this HttpMethod httpMethod, TInput input)
        where TInput : class
    {
        return (httpMethod.Method == HttpMethods.Post || httpMethod.Method == HttpMethods.Put) && input is not null;
    }
}