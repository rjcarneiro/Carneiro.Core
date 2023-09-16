namespace Carneiro.Core.Host;

/// <summary>
/// Settings for <see cref="PeriodicBackgroundService"/>.
/// </summary>
public class PeriodicBackgroundServiceOptions
{
    /// <summary>
    /// Gets or sets the minimum time, in seconds, for the job to wait until the next iteration.
    /// </summary>
    public int Min { get; set; } = 30;

    /// <summary>
    /// Gets or sets the maximum time, in seconds, for the job to wait until the next iteration.
    /// </summary>
    public int Max { get; set; } = 150;

    /// <inheritdoc />
    public override string ToString() => $"{nameof(Min)}: {Min}, {nameof(Max)}: {Max}";
}