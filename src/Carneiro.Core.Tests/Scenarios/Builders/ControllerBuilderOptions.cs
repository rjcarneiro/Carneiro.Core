using Microsoft.Extensions.Primitives;

namespace Carneiro.Core.Tests.Scenarios.Builders;

/// <summary>
/// The <see cref="IControllerBuilderOptions"/> implementation.
/// </summary>
public class ControllerBuilderOptions : IControllerBuilderOptions
{
    /// <summary>
    /// Gets the cookies.
    /// </summary>
    /// <value>
    /// The cookies.
    /// </value>
    public IDictionary<string, string> Cookies { get; } = new Dictionary<string, string>();

    /// <inheritdoc />
    public Dictionary<string, StringValues> Form { get; } = new();
}