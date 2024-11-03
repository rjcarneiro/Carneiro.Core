using Carneiro.Core.Web.Extensions;

namespace Carneiro.Core.Web;

/// <summary>
/// Default Http Orchestrator implementation.
/// </summary>
/// <seealso cref="IHttpOrchestrator" />
public class HttpOrchestrator : IHttpOrchestrator
{
    private readonly ILogger<HttpOrchestrator> _logger;

    /// <summary>
    /// The Http Client
    /// </summary>
    protected HttpClient HttpClient { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpOrchestrator" /> class.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="logger">The logger.</param>
    public HttpOrchestrator(HttpClient client, ILogger<HttpOrchestrator> logger)
    {
        HttpClient = client;
        _logger = logger;
    }

    /// <inheritdoc />
    public virtual Task<HttpResponseMessage> GetAsync(string uri) => SendHttpRequestAsync(HttpMethod.Get, uri, errorAction: null);

    /// <inheritdoc />
    public virtual Task<HttpResponseMessage> GetAsync(string uri, CancellationToken cancellationToken) => SendHttpRequestAsync(HttpMethod.Get, uri, cancellationToken);

    /// <inheritdoc />
    public virtual Task<TOutput> GetAsync<TOutput>(string uri) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Get, uri);

    /// <inheritdoc />
    public virtual Task<TOutput> GetAsync<TOutput>(string uri, CancellationToken cancellationToken) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Get, uri, cancellationToken);

    /// <inheritdoc />
    public virtual Task<HttpResponseMessage> GetAsync(string uri, Action<Exception> action) => SendHttpRequestAsync(HttpMethod.Get, uri, action);

    /// <inheritdoc />
    public virtual Task<HttpResponseMessage> GetAsync(string uri, Action<Exception> action, CancellationToken cancellationToken) => SendHttpRequestAsync(HttpMethod.Get, uri, action, cancellationToken);

    /// <inheritdoc />
    public virtual Task<TOutput> GetAsync<TOutput>(string uri, Action<Exception> action) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Get, uri, action);

    /// <inheritdoc />
    public virtual Task PostAsync(string uri) => SendHttpRequestAsync(HttpMethod.Post, uri);

    /// <inheritdoc />
    public virtual Task PostAsync(string uri, CancellationToken cancellationToken) => SendHttpRequestAsync(HttpMethod.Post, uri, cancellationToken);

    /// <inheritdoc />
    public virtual Task<TOutput> PostAsync<TOutput>(string uri) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Post, uri);

    /// <inheritdoc />
    public virtual Task<TOutput> PostAsync<TOutput>(string uri, CancellationToken cancellationToken) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Post, uri, cancellationToken);

    /// <inheritdoc />
    public virtual Task<TOutput> PostAsync<TOutput>(string uri, Action<Exception> action) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Post, uri, action);

    /// <inheritdoc />
    public virtual Task<TOutput> PostAsync<TOutput>(string uri, Action<Exception> action, CancellationToken cancellationToken) where TOutput : class =>
        SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Post, uri, action, cancellationToken);

    /// <inheritdoc />
    public virtual Task PostAsync<TInput>(string uri, TInput model) where TInput : class => SendHttpRequestAsync(HttpMethod.Post, uri, model);

    /// <inheritdoc />
    public virtual Task PostAsync<TInput>(string uri, TInput model, Action<Exception> action) where TInput : class => SendHttpRequestAsync(HttpMethod.Post, uri, model, action);

    /// <inheritdoc />
    public virtual Task<TOutput> PostAsync<TInput, TOutput>(string uri, TInput model)
        where TInput : class
        where TOutput : class
        => SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod.Post, uri, model);

    /// <inheritdoc />
    public virtual Task<TOutput> PostAsync<TInput, TOutput>(string uri, TInput model, Action<Exception> action)
        where TInput : class
        where TOutput : class
        => SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod.Post, uri, model, action);

    /// <inheritdoc />
    public virtual Task<TOutput> PutAsync<TOutput>(string uri) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Put, uri);

    /// <inheritdoc />
    public virtual Task<TOutput> PutAsync<TOutput>(string uri, Action<Exception> action) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Put, uri, action);

    /// <inheritdoc />
    public virtual Task PutAsync<TInput>(string uri, TInput model) where TInput : class => SendHttpRequestAsync(HttpMethod.Put, uri, model);

    /// <inheritdoc />
    public virtual Task PutAsync<TInput>(string uri, TInput model, Action<Exception> action) where TInput : class => SendHttpRequestAsync(HttpMethod.Put, uri, model, action);

    /// <inheritdoc />
    public virtual Task<TOutput> PutAsync<TInput, TOutput>(string uri, TInput model)
        where TInput : class
        where TOutput : class
        => SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod.Put, uri, model);

    /// <inheritdoc />
    public virtual Task<TOutput> PutAsync<TInput, TOutput>(string uri, TInput model, Action<Exception> action)
        where TInput : class
        where TOutput : class
        => SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod.Put, uri, model, action);

    /// <inheritdoc />
    public virtual async Task DeleteAsync(string uri) => await SendHttpRequestAsync(HttpMethod.Delete, uri);

    /// <inheritdoc />
    public virtual async Task DeleteAsync(string uri, Action<Exception> action) => await SendHttpRequestAsync(HttpMethod.Delete, uri, action);

    private async Task<TOutput> SendHttpRequestWithContentAsync<TOutput>(HttpMethod httpMethod, string url)
        where TOutput : class
    {
        return await SendHttpRequestWithContentAsync<TOutput>(httpMethod, url, errorAction: null, cancellationToken: default);
    }

    private async Task<TOutput> SendHttpRequestWithContentAsync<TOutput>(HttpMethod httpMethod, string url, CancellationToken cancellationToken)
        where TOutput : class
    {
        return await SendHttpRequestWithContentAsync<TOutput>(httpMethod, url, errorAction: null, cancellationToken: cancellationToken);
    }

    private async Task<TOutput> SendHttpRequestWithContentAsync<TOutput>(HttpMethod httpMethod, string url, Action<Exception> action)
        where TOutput : class
    {
        return await SendHttpRequestWithContentAsync<TOutput>(httpMethod, url, action, cancellationToken: default);
    }

    private async Task<TOutput> SendHttpRequestWithContentAsync<TOutput>(HttpMethod httpMethod, string url, Action<Exception> errorAction, CancellationToken cancellationToken)
        where TOutput : class
    {
        HttpResponseMessage response = await SendHttpRequestAsync(httpMethod, url, errorAction, cancellationToken);
        return await response.GetContentAsync<TOutput>();
    }

    private async Task<TOutput> SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod httpMethod, string url, TInput model)
        where TInput : class
        where TOutput : class
    {
        HttpResponseMessage response = await SendHttpRequestAsync(httpMethod, url, model);
        return await response.GetContentAsync<TOutput>();
    }

    private async Task<TOutput> SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod httpMethod, string url, TInput model, Action<Exception> action)
        where TInput : class
        where TOutput : class
    {
        HttpResponseMessage response = await SendHttpRequestAsync(httpMethod, url, model, action);
        return await response.GetContentAsync<TOutput>();
    }

    private Task<HttpResponseMessage> SendHttpRequestAsync(HttpMethod httpMethod, string url)
        => SendHttpRequestAsync<string>(httpMethod, url, input: null, errorAction: null);

    private Task<HttpResponseMessage> SendHttpRequestAsync(HttpMethod httpMethod, string url, Action<Exception> errorAction)
        => SendHttpRequestAsync<string>(httpMethod, url, input: null, errorAction: errorAction);

    private Task<HttpResponseMessage> SendHttpRequestAsync(HttpMethod httpMethod, string url, Action<Exception> errorAction, CancellationToken cancellationToken)
        => SendHttpRequestAsync<string>(httpMethod, url, input: null, errorAction, cancellationToken: cancellationToken);

    private Task<HttpResponseMessage> SendHttpRequestAsync(HttpMethod httpMethod, string url, CancellationToken cancellationToken)
        => SendHttpRequestAsync<string>(httpMethod, url, input: null, errorAction: null, cancellationToken: cancellationToken);

    private async Task<HttpResponseMessage> SendHttpRequestAsync<TInput>(HttpMethod httpMethod, string url, TInput input = null, Action<Exception> errorAction = null,
        CancellationToken cancellationToken = default)
        where TInput : class
    {
        _logger.LogInformation("Creating http request {HttpMethodMethod} {HttpClientBaseAddress} => {Url}", httpMethod.Method, HttpClient.BaseAddress, url);

        using var request = new HttpRequestMessage(httpMethod, url);

        if (httpMethod.AllowsBody(input))
        {
            request.Content = new StringContent(JsonHelper.Serialize(input), Encoding.UTF8, "application/json");
        }

        HttpResponseMessage response;

        try
        {
            response = await HttpClient.SendAsync(request, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unexpected error while making a {HttpMethod} into '{Url}'", httpMethod, url);
            errorAction?.Invoke(e);
            throw;
        }

        if (response.IsSuccessStatusCode)
        {
            return response;
        }

        // something went wrong
        var httpContent = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogWarning("Http response with failed status code {StatusCode} and body '{HttpContent}'", response.StatusCode, httpContent);

        throw new HttpErrorResponseRzException(response.StatusCode, httpContent);
    }
}