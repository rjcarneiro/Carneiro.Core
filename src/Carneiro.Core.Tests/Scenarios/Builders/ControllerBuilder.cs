namespace Carneiro.Core.Tests.Scenarios.Builders;

/// <summary>
/// The <see cref="IControllerBuilder"/> implementation.
/// </summary>
public class ControllerBuilder : IControllerBuilder
{
    private readonly IControllerBuilderOptions _controllerBuilderOptions;

    private ControllerBuilder()
    {
        _controllerBuilderOptions = new ControllerBuilderOptions();
    }

    /// <inheritdoc/>
    public IControllerBuilder WithCookie(string key, string value)
    {
        _controllerBuilderOptions.Cookies.Add(key, value);
        return this;
    }
        
    /// <inheritdoc/>
    public IControllerBuilder WithForm(string key, string value)
    {
        _controllerBuilderOptions.Form.Add(key, value);
        return this;
    }

    /// <summary>
    /// Builds this instance.
    /// </summary>
    public IControllerBuilderOptions Build() => _controllerBuilderOptions;

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public static IControllerBuilder Init() => new ControllerBuilder();
}