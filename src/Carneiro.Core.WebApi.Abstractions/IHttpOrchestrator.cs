namespace Carneiro.Core.WebApi.Abstractions;

/// <summary>
/// Interface that abstracts the complexity of using default <see cref="HttpClient"/>.
/// </summary>
public interface IHttpOrchestrator
{
    /// <summary>
    /// Creates a new <see cref="HttpMethod.Get"/> request.
    /// </summary>
    /// <param name="uri">The URI.</param>
    Task GetAsync(string uri);

    /// <summary>
    /// Creates a new <see cref="HttpMethod.Get"/> request.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellationToken"></param>
    Task GetAsync(string uri, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new <see cref="HttpMethod.Get"/> request.
    /// </summary>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    /// <param name="uri">The URI.</param>
    Task<TOutput> GetAsync<TOutput>(string uri) where TOutput : class;

    /// <summary>
    /// Creates a new <see cref="HttpMethod.Get"/> request.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="action"></param>
    /// <typeparam name="TOutput"></typeparam>
    Task<TOutput> GetAsync<TOutput>(string uri, Action<Exception> action) where TOutput : class;

    /// <summary>
    /// Creates a new <see cref="HttpMethod.Get"/> request.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TOutput"></typeparam>
    Task<TOutput> GetAsync<TOutput>(string uri, CancellationToken cancellationToken) where TOutput : class;

    /// <summary>
    /// Creates a new <see cref="HttpMethod.Get"/> request.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="errorAction">The action.</param>
    /// <returns></returns>
    Task GetAsync(string uri, Action<Exception> errorAction);

    /// <summary>
    /// Creates a new <see cref="HttpMethod.Get"/> request.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    Task GetAsync(string uri, Action<Exception> action, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new <see cref="HttpMethod.Post"/> request.
    /// </summary>
    /// <param name="uri">The URI.</param>
    Task PostAsync(string uri);
    
    /// <summary>
    /// Creates a new <see cref="HttpMethod.Post"/> request.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellationToken"></param>
    Task PostAsync(string uri, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new <see cref="HttpMethod.Post"/> request.
    /// </summary>
    /// <param name="uri">The URI.</param>
    Task<TOutput> PostAsync<TOutput>(string uri) where TOutput : class;
    
    /// <summary>
    /// Creates a new <see cref="HttpMethod.Post"/> request.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TOutput"></typeparam>
    /// <returns></returns>
    Task<TOutput> PostAsync<TOutput>(string uri, CancellationToken cancellationToken) where TOutput : class;

    /// <summary>
    /// Creates a new <see cref="HttpMethod.Post"/> request.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="errorAction">The specific error handling action case something goes wrong.</param>
    Task<TOutput> PostAsync<TOutput>(string uri, Action<Exception> errorAction) where TOutput : class;
    
    /// <summary>
    /// Creates a new <see cref="HttpMethod.Post"/> request.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TOutput"></typeparam>
    Task<TOutput> PostAsync<TOutput>(string uri, Action<Exception> action, CancellationToken cancellationToken) where TOutput : class;

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    Task PostAsync<TInput>(string uri, TInput model) where TInput : class;

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The action.</param>
    Task PostAsync<TInput>(string uri, TInput model, Action<Exception> action) where TInput : class;

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    Task<TOutput> PostAsync<TInput, TOutput>(string uri, TInput model) where TInput : class where TOutput : class;

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    Task<TOutput> PostAsync<TInput, TOutput>(string uri, TInput model, Action<Exception> action) where TInput : class where TOutput : class;

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    Task<TOutput> PutAsync<TOutput>(string uri) where TOutput : class;

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    Task<TOutput> PutAsync<TOutput>(string uri, Action<Exception> action) where TOutput : class;

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    Task PutAsync<TInput>(string uri, TInput model) where TInput : class;

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    Task PutAsync<TInput>(string uri, TInput model, Action<Exception> action) where TInput : class;

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    Task<TOutput> PutAsync<TInput, TOutput>(string uri, TInput model) where TInput : class where TOutput : class;

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
    Task<TOutput> PutAsync<TInput, TOutput>(string uri, TInput model, Action<Exception> action) where TInput : class where TOutput : class;

    /// <summary>
    /// Performs a request using the DELETE verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    Task DeleteAsync(string uri);

    /// <summary>
    /// Performs a request using the DELETE verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
    Task DeleteAsync(string uri, Action<Exception> action);
}