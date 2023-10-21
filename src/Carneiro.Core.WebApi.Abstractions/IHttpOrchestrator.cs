namespace Carneiro.Core.WebApi.Abstractions;

/// <summary>
/// Interface that communicates with micro services.
/// </summary>
public interface IHttpOrchestrator
{
    /// <summary>
    /// Gets the asynchronous.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    Task GetAsync(string uri);

    /// <summary>
    /// Performs a request using the GET verb asynchronously
    /// </summary>
    /// <typeparam name="TOutput">The type of the output.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    Task<TOutput> GetAsync<TOutput>(string uri) where TOutput : class;

    /// <summary>
    /// Performs a request using the GET verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    Task GetAsync(string uri, Action<Exception> action);

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    Task<TOutput> PostAsync<TOutput>(string uri) where TOutput : class;

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    Task PostAsync(string uri);

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
    Task<TOutput> PostAsync<TOutput>(string uri, Action<Exception> action) where TOutput : class;

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    Task PostAsync<TInput>(string uri, TInput model) where TInput : class;

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    Task PostAsync<TInput>(string uri, TInput model, Action<Exception> action) where TInput : class;

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    Task<TOutput> PostAsync<TInput, TOutput>(string uri, TInput model) where TInput : class where TOutput : class;

    /// <summary>
    /// Performs a request using the POST verb asynchronously
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <typeparam name="TOutput">The output class object.</typeparam>
    /// <param name="uri">The URI.</param>
    /// <param name="model">The model.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
    Task<TOutput> PostAsync<TInput, TOutput>(string uri, TInput model, Action<Exception> action) where TInput : class where TOutput : class;

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    Task<TOutput> PutAsync<TOutput>(string uri) where TOutput : class;

    /// <summary>
    /// Performs a request using the Put verb asynchronously
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="action">The specific error handling action case something goes wrong.</param>
    /// <returns></returns>
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