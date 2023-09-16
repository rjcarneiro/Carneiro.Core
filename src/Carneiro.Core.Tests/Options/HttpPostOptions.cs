using Carneiro.Core.Tests.Core;

namespace Carneiro.Core.Tests.Options;

/// <summary>
/// Options for sending an <see cref="HttpMethod.Post"/> from <see cref="AjaxRequestHandler.PostAsync"/> methods.
/// </summary>
public class HttpPostOptions
{
    /// <summary>
    /// Sends the <see cref="HttpMethod.Post"/> as json.
    /// </summary>
    public bool SendAsJson { get; set; }

    /// <summary>
    /// Sends the <see cref="HttpMethod.Post"/> as form encoded.
    /// </summary>
    public bool SendAsForm { get; set; }

    /// <inheritdoc />
    public override string ToString() => $"{nameof(SendAsJson)}: {SendAsJson}, {nameof(SendAsForm)}: {SendAsForm}";
}