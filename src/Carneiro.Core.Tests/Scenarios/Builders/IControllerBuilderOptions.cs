using Microsoft.Extensions.Primitives;

namespace Carneiro.Core.Tests.Scenarios.Builders;

/// <summary>
/// The controller builder options interface.
/// </summary>
public interface IControllerBuilderOptions
{
    /// <summary>
    /// Gets the cookies.
    /// </summary>
    /// <value>
    /// The cookies.
    /// </value>
    IDictionary<string, string> Cookies { get; }

    /// <summary>
    /// Gets the form.
    /// </summary>
    /// <value>
    /// The form.
    /// </value>
    Dictionary<string, StringValues> Form { get; }
}