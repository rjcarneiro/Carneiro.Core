namespace Carneiro.Core.Tests.Scenarios.Builders;

/// <summary>
/// The controller builder.
/// </summary>
public interface IControllerBuilder
{
    /// <summary>
    /// Adds a cookie with name <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    IControllerBuilder WithCookie(string key, string value);

    /// <summary>
    /// Adds a new item into the Request.Form dictionary.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public IControllerBuilder WithForm(string key, string value);

    /// <summary>
    /// Builds this instance.
    /// </summary>
    /// <returns></returns>
    IControllerBuilderOptions Build();
}