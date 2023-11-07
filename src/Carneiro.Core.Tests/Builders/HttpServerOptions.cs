namespace Carneiro.Core.Tests.Builders;

/// <summary>
/// Sets the options for <see cref="IBaseScenarioBuilder.StartHttpServer"/>.
/// </summary>
public class HttpServerOptions
{
    /// <summary>
    /// Sets a fake anti forgery token verification.
    /// </summary>
    public bool WithFakeAntiForgery { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether [authentication at startup].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [authentication at startup]; otherwise, <c>false</c>.
    /// </value>
    public bool AuthenticationAtStartup { get; set; } = true;
}