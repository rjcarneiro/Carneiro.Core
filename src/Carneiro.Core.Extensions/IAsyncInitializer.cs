namespace Carneiro.Core.Extensions;

/// <summary>
/// Interface to start an async initializer.
/// </summary>
public interface IAsyncInitializer
{
    /// <summary>
    /// Initializes the work to be done before the application starts.
    /// </summary>
    Task InitializeAsync();
}