namespace Carneiro.Core.Tests.Core;

/// <summary>
/// Class that sends ajax requests.
/// </summary>
public class AjaxRequestHandler
{
    private readonly HttpClient _httpClient;
    private AntiForgeryTokenOptions _antiForgeryTokenOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="AjaxRequestHandler"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public AjaxRequestHandler(HttpClient httpClient) => _httpClient = httpClient;

    /// <summary>
    /// Gets the asynchronously.
    /// </summary>
    /// <param name="uri">The URI.</param>
    public virtual Task<HttpResponseMessage> GetAsync(string uri) => _httpClient.GetAsync(uri);

    /// <summary>
    /// Gets the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uri">The URI.</param>
    public virtual Task<T> GetAsync<T>(string uri) => RequestAsync<T>(HttpMethod.Get, uri);

    /// <summary>
    /// Gets the binary asynchronously.
    /// </summary>
    /// <param name="uri">The URI.</param>
    public virtual async Task<byte[]> GetBinaryAsync(string uri)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }

    /// <summary>
    /// Sends an <see cref="HttpMethod.Post"/> request asynchronously.
    /// </summary>
    /// <param name="uri">The URI.</param>
    public virtual Task<HttpResponseMessage> PostAsync(string uri) => MakePostWithAntiForgeryTokenAsync<string>(uri, null, new HttpPostOptions { SendAsJson = true });

    /// <summary>
    /// Sends an <see cref="HttpMethod.Post"/> request asynchronously.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    public virtual Task<HttpResponseMessage> PostAsync<T>(string uri, T model) where T : class => MakePostWithAntiForgeryTokenAsync<T>(uri, model, new HttpPostOptions
    {
        SendAsForm = true
    });

    /// <summary>
    /// Sends an <see cref="HttpMethod.Post"/> request with certain <paramref name="options"/> asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uri"></param>
    /// <param name="model"></param>
    /// <param name="options"></param>
    public virtual Task<HttpResponseMessage> PostAsync<T>(string uri, T model, Action<HttpPostOptions> options) where T : class
    {
        var o = new HttpPostOptions();
        options(o);

        return MakePostWithAntiForgeryTokenAsync<T>(uri, model, o);
    }

    /// <summary>
    /// Sends an <see cref="HttpMethod.Put"/> request asynchronously.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    public virtual Task<HttpResponseMessage> PutAsync(string uri, object model = null) => _httpClient.PutAsync(uri, new StringContent(JsonHelper.Serialize(model), Encoding.UTF8, "application/json"));

    /// <summary>
    /// Puts the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    public virtual Task<T> PutAsync<T>(string uri, object model = null) where T : class => RequestAsync<T>(HttpMethod.Put, uri, model);

    /// <summary>
    /// Sends an <see cref="HttpMethod.Post"/> request asynchronously.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    public virtual Task<HttpResponseMessage> PutAsFormAsync(string uri, Dictionary<string, string> model) => _httpClient.PutAsync(uri, new FormUrlEncodedContent(model));

    /// <summary>
    /// Sends an <see cref="HttpMethod.Patch"/> request asynchronously.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    public virtual Task<HttpResponseMessage> PatchAsync(string uri, object model = null) => _httpClient.PatchAsync(uri, new StringContent(JsonHelper.Serialize(model), Encoding.UTF8, "application/json"));

    /// <summary>
    /// Sends an <see cref="HttpMethod.Patch"/> request asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    public virtual Task<T> PatchAsync<T>(string uri, object model = null) where T : class => RequestAsync<T>(HttpMethod.Patch, uri, model);

    /// <summary>
    /// Deletes the asynchronously.
    /// </summary>
    /// <param name="uri">The URI.</param>
    public virtual Task<HttpResponseMessage> DeleteAsync(string uri) => _httpClient.DeleteAsync(uri);

    /// <summary>
    /// Uploads the asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="filePath">The file path.</param>
    public virtual async Task<T> UploadAsync<T>(string uri, string filePath)
    {
        using var content = new MultipartFormDataContent();
        await using FileStream stream = File.OpenRead(filePath);
        var fileContent = new ByteArrayContent(await new StreamContent(stream).ReadAsByteArrayAsync());
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

        content.Add(fileContent, "profileImage", "image.png");

        using HttpResponseMessage message = await _httpClient.PostAsync(uri, content);
        message.EnsureSuccessStatusCode();
        return JsonHelper.Deserialize<T>(await message.Content.ReadAsStringAsync());
    }

    /// <summary>
    /// Sets the authentication.
    /// </summary>
    /// <param name="httpResponseMessage">The http response message.</param>
    public virtual void SetAuthentication(HttpResponseMessage httpResponseMessage) => SetAuthentication(httpResponseMessage.GetAuthenticationCookieValue());

    /// <summary>
    /// Sets the authentication.
    /// </summary>
    /// <param name="cookie">The cookie.</param>

    public virtual void SetAuthentication(string cookie) => _httpClient.DefaultRequestHeaders.Add(HeaderNames.Cookie, cookie);

    /// <summary>
    /// Sets the anti forgery token.
    /// </summary>
    /// <param name="antiForgeryTokenOptions">The anti forgery token set.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual void SetAntiForgeryToken(AntiForgeryTokenOptions antiForgeryTokenOptions) => _antiForgeryTokenOptions = antiForgeryTokenOptions
        ?? throw new ArgumentNullException(nameof(antiForgeryTokenOptions));

    /// <summary>
    /// Before it sends any scenario http request, this method is called asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpMethod">The HTTP method.</param>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    protected virtual Task<T> RequestAsync<T>(HttpMethod httpMethod, string uri, object model = null)
        => RequestAsync<T>(httpMethod, uri, new StringContent(JsonHelper.Serialize(model), Encoding.UTF8, "application/json"));

    private async Task<T> RequestAsync<T>(HttpMethod httpMethod, string uri, HttpContent content)
    {
        HttpResponseMessage response = null;

        if (httpMethod == HttpMethod.Post)
            response = await _httpClient.PostAsync(uri, content);

        if (httpMethod == HttpMethod.Put)
            response = await _httpClient.PutAsync(uri, content);

        if (httpMethod == HttpMethod.Get)
            response = await _httpClient.GetAsync(uri);

        if (httpMethod == HttpMethod.Patch)
            response = await _httpClient.PatchAsync(uri, content);

        if (response is null)
            throw new ApplicationException("null response");

        if (response.IsSuccessStatusCode)
            return await response.GetContentAsync<T>();

        throw new ApplicationException(await response.Content.ReadAsStringAsync());
    }

    private Dictionary<string, string> GetAndSetAntiForgeryToken()
    {
        if (_antiForgeryTokenOptions is null)
            return null;

        _httpClient.DefaultRequestHeaders.Add(_antiForgeryTokenOptions.HeaderName!, _antiForgeryTokenOptions.RequestToken);
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Cookie, new CookieHeaderValue(_antiForgeryTokenOptions.CookieName, _antiForgeryTokenOptions.CookieToken).ToString());

        return new Dictionary<string, string>
        {
            { _antiForgeryTokenOptions.FormFieldName, _antiForgeryTokenOptions.RequestToken }
        };
    }

    private void ClearSetAntiForgeryToken()
    {
        if (_antiForgeryTokenOptions is not null)
        {
            _httpClient.DefaultRequestHeaders.Remove(_antiForgeryTokenOptions.HeaderName);
            if (_httpClient.DefaultRequestHeaders.TryGetValues(HeaderNames.Cookie, out IEnumerable<string> cookies))
            {
                cookies = cookies.Where(t => !t.Contains(_antiForgeryTokenOptions.CookieName));
                _httpClient.DefaultRequestHeaders.Remove(HeaderNames.Cookie);
                _httpClient.DefaultRequestHeaders.Add(HeaderNames.Cookie, cookies);
            }

            _antiForgeryTokenOptions = null;
        }
    }

    private async Task<HttpResponseMessage> MakePostWithAntiForgeryTokenAsync<T>(string uri, T model, HttpPostOptions httpPostOptions) where T : class
    {
        Dictionary<string, string> bodyRequest = GetAndSetAntiForgeryToken();

        if (model is not null)
        {
            Dictionary<string, string> formModel = JsonHelper.Deserialize<Dictionary<string, string>>(JsonHelper.Serialize(model));

            if (bodyRequest is not null)
            {
                foreach ((string key, string value) in formModel)
                {
                    bodyRequest.Add(key, value);
                }
            }
            else
            {
                bodyRequest = formModel;
            }
        }

        HttpResponseMessage response;

        // at this point, there's no forgery token nor a model
        if (bodyRequest is null)
        {
            response = await _httpClient.PostAsync(uri, new StringContent(string.Empty, Encoding.UTF8, "application/json"));
        }
        else
        {
            if (httpPostOptions.SendAsJson)
                response = await _httpClient.PostAsJsonAsync(uri, bodyRequest);
            else if (httpPostOptions.SendAsForm)
                response = await _httpClient.PostAsync(uri, new FormUrlEncodedContent(bodyRequest));
            else
                throw new ApplicationException("Cannot decide either to have post as json or post as form");
        }

        ClearSetAntiForgeryToken();

        return response;
    }
}