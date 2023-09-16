namespace Carneiro.Core.Tests.Options;

/// <summary>
/// The options for Anti Forgery tokens.
/// </summary>
public class AntiForgeryTokenOptions
{
    /// <summary>
    /// Gets or sets the name of the header.
    /// </summary>
    /// <value>
    /// The name of the header.
    /// </value>
    public string HeaderName { get; init; }

    /// <summary>
    /// Gets or sets the cookie.
    /// </summary>
    /// <value>
    /// The cookie.
    /// </value>
    public string CookieName { get; init; }

    /// <summary>
    /// Gets or sets the name of the form field.
    /// </summary>
    /// <value>
    /// The name of the form field.
    /// </value>
    public string FormFieldName { get; init; }

    /// <summary>
    /// Gets or sets the request token.
    /// </summary>
    /// <value>
    /// The request token.
    /// </value>
    public string RequestToken { get; init; }

    /// <summary>
    /// Gets or sets the cookie token.
    /// </summary>
    /// <value>
    /// The cookie token.
    /// </value>
    public string CookieToken { get; init; }

    /// <inheritdoc />
    public override string ToString() => $"{nameof(HeaderName)}: {HeaderName}, {nameof(CookieName)}: {CookieName}, {nameof(FormFieldName)}: {FormFieldName}, {nameof(RequestToken)}: {RequestToken}, {nameof(CookieToken)}: {CookieToken}";
}