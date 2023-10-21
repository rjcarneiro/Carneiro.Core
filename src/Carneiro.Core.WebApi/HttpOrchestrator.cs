namespace Carneiro.Core.WebApi;

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

    #region Get

    /// <summary>
    /// Performs a request using the GET verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    public virtual Task GetAsync(string uri) => SendHttpRequestAsync(HttpMethod.Get, uri);

    /// <summary>
    /// Performs a request using the GET verb asynchronously
    /// </summary>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    public virtual Task<TOutput> GetAsync<TOutput>(string uri) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Get, uri);

    /// <summary>
    /// Performs a request using the GET verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public virtual Task GetAsync(string uri, Action<Exception> action) => SendHttpRequestAsync(HttpMethod.Get, uri, action);

    /// <summary>
    /// Performs a request using the GET verb asynchronously
    /// </summary>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public virtual Task<TOutput> GetAsync<TOutput>(string uri, Action<Exception> action) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Get, uri, action);

    #endregion Get

    #region Post

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    public virtual Task<TOutput> PostAsync<TOutput>(string uri) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Post, uri);

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    public virtual Task PostAsync(string uri) => SendHttpRequestAsync(HttpMethod.Post, uri);

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
    public virtual Task<TOutput> PostAsync<TOutput>(string uri, Action<Exception> action) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Post, uri, action);

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    public virtual Task PostAsync<TInput>(string uri, TInput model) where TInput : class => SendHttpRequestAsync(HttpMethod.Post, uri, model);

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public virtual Task PostAsync<TInput>(string uri, TInput model, Action<Exception> action) where TInput : class => SendHttpRequestAsync(HttpMethod.Post, uri, model, action);

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    public virtual Task<TOutput> PostAsync<TInput, TOutput>(string uri, TInput model)
        where TInput : class
        where TOutput : class
        => SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod.Post, uri, model);

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
    public virtual Task<TOutput> PostAsync<TInput, TOutput>(string uri, TInput model, Action<Exception> action)
        where TInput : class
        where TOutput : class
        => SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod.Post, uri, model, action);

    #endregion Post

    #region Put

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    public virtual Task<TOutput> PutAsync<TOutput>(string uri) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Put, uri);

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
    public virtual Task<TOutput> PutAsync<TOutput>(string uri, Action<Exception> action) where TOutput : class => SendHttpRequestWithContentAsync<TOutput>(HttpMethod.Put, uri, action);

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    public virtual Task PutAsync<TInput>(string uri, TInput model) where TInput : class => SendHttpRequestAsync(HttpMethod.Put, uri, model);

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public virtual Task PutAsync<TInput>(string uri, TInput model, Action<Exception> action) where TInput : class => SendHttpRequestAsync(HttpMethod.Put, uri, model, action);

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    public virtual Task<TOutput> PutAsync<TInput, TOutput>(string uri, TInput model)
        where TInput : class
        where TOutput : class
        => SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod.Put, uri, model);

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
    public virtual Task<TOutput> PutAsync<TInput, TOutput>(string uri, TInput model, Action<Exception> action)
        where TInput : class
        where TOutput : class
        => SendHttpRequestWithContentAsync<TInput, TOutput>(HttpMethod.Put, uri, model, action);

    #endregion Put

    #region Delete

    /// <summary>
    /// Performs a request using the DELETE verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(string uri) => await SendHttpRequestAsync(HttpMethod.Delete, uri);

    /// <summary>
    /// Performs a request using the DELETE verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(string uri, Action<Exception> action) => await SendHttpRequestAsync(HttpMethod.Delete, uri, action);

    #endregion Delete

    private static StringContent GetStringContent<TInput>(TInput model) where TInput : class => new(JsonHelper.Serialize(model), Encoding.UTF8, "application/json");

    private async Task<TOutput> SendHttpRequestWithContentAsync<TOutput>(HttpMethod httpMethod, string url)
        where TOutput : class
    {
        HttpResponseMessage response = await SendHttpRequestAsync(httpMethod, url);
        return await response.GetContentAsync<TOutput>();
    }

    private async Task<TOutput> SendHttpRequestWithContentAsync<TOutput>(HttpMethod httpMethod, string url, Action<Exception> action)
        where TOutput : class
    {
        HttpResponseMessage response = await SendHttpRequestAsync(httpMethod, url, action);
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

    private Task<HttpResponseMessage> SendHttpRequestAsync(HttpMethod httpMethod, string url, Action<Exception> action) => SendHttpRequestAsync<string>(httpMethod, url, model: null, action: action);

    private Task<HttpResponseMessage> SendHttpRequestAsync(HttpMethod httpMethod, string url) => SendHttpRequestAsync<string>(httpMethod, url, model: null, action: null);

    private async Task<HttpResponseMessage> SendHttpRequestAsync<TInput>(HttpMethod httpMethod, string url, TInput model = null, Action<Exception> action = null) 
        where TInput : class
    {
        _logger.LogInformation("Creating http request {HttpMethodMethod} {HttpClientBaseAddress} => {Url}", httpMethod.Method, HttpClient.BaseAddress, url);
        using var request = new HttpRequestMessage(httpMethod, url);

        if ((httpMethod.Method == HttpMethods.Post || httpMethod.Method == HttpMethods.Put) && model != null)
        {
            request.Content = GetStringContent(model);
        }

        HttpResponseMessage response;

        try
        {
            response = await HttpClient.SendAsync(request);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unexpected error while making a broker HTTP request");

            // handle custom exception action first
            action?.Invoke(e);

            // normal tiny http error handling
            if (e is HttpRequestException)
                throw new HttpConnectivityRzException(e);

            throw new HttpRzException(e);
        }

        if (response.IsSuccessStatusCode)
        {
            return response;
        }

        // something went wrong
        _logger.LogWarning("Http response with failed status code: {ResponseStatusCode}", response.StatusCode);
        var httpContent = await response.Content.ReadAsStringAsync();
        _logger.LogWarning("Http response with body: {HttpContent}", httpContent);

        HttpRzException httpRzException = !string.IsNullOrEmpty(httpContent)
            ? new HttpRzException(response.StatusCode, JsonHelper.Deserialize<ApiErrorResponse>(httpContent))
            : new HttpRzException(response.StatusCode);

        // custom behaviour 
        // if it's set, should throw an exception and the following code is not reached
        action?.Invoke(httpRzException);

        // default behaviour case 'action' is not set or doesn't match
        throw httpRzException;
    }
}
