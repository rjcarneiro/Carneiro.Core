namespace Carneiro.Core.Repository.Options;

/// <summary>
/// Database options.
/// </summary>
public class DatabaseOptions
{
    /// <summary>
    /// Gets or sets the timeout, in seconds. Default is 60 seconds.
    /// </summary>
    /// <value>
    /// The timeout.
    /// </value>
    public int Timeout { get; set; } = 60;

    /// <summary>
    /// Gets or sets the failure options.
    /// </summary>
    /// <value>
    /// The failure options.
    /// </value>
    public DatabaseFailureOptions Failure { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether [enable detailed errors].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [enable detailed errors]; otherwise, <c>false</c>.
    /// </value>
    public bool EnableDetailedErrors { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether [enable sensitive data logging].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [enable sensitive data logging]; otherwise, <c>false</c>.
    /// </value>
    public bool EnableSensitiveDataLogging { get; set; } = false;

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() => $"{nameof(Timeout)}: {Timeout}, {nameof(Failure)}: {Failure}, {nameof(EnableDetailedErrors)}: {EnableDetailedErrors}, {nameof(EnableSensitiveDataLogging)}: {EnableSensitiveDataLogging}";
}