namespace Carneiro.Core.Repository.Options;

/// <summary>
/// Database failure options.
/// </summary>
public class DatabaseFailureOptions
{
    /// <summary>
    /// Gets or sets the max number of retries. Default is 3 attempts.
    /// </summary>
    /// <value>
    /// The max number of retries.
    /// </value>
    public int Retries { get; set; } = 3;

    /// <summary>
    /// Gets or sets the seconds per each retry. Default is 15 seconds.
    /// </summary>
    /// <value>
    /// The seconds per each retry.
    /// </value>
    public int Seconds { get; set; } = 15;

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() => $"{nameof(Retries)}: {Retries}, {nameof(Seconds)}: {Seconds}";
}