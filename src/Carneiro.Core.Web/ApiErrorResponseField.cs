namespace Carneiro.Core.Web;

/// <summary>
/// Api Error Response Field.
/// </summary>
public class ApiErrorResponseField
{
    /// <summary>
    /// Gets or sets the field.
    /// </summary>
    /// <value>
    /// The field.
    /// </value>
    public string Field { get; set; }

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    public string Message { get; set; }

    /// <inheritdoc />
    public override string ToString() => $"{nameof(Field)}: {Field}, {nameof(Message)}: {Message}";
}