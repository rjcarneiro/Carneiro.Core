/// <summary>
/// Database options class.
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
    /// Gets or sets the failure settings.
    /// </summary>
    /// <value>
    /// The failure settings.
    /// </value>
    public DatabaseFailureOptions Failure { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether [enable sensitive data logging].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [enable sensitive data logging]; otherwise, <c>false</c>.
    /// </value>
    public bool EnableSensitiveDataLogging { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether [enable detailed errors].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [enable detailed errors]; otherwise, <c>false</c>.
    /// </value>
    public bool EnableDetailedErrors { get; set; } = false;

    /// <summary>
    /// Gets or sets the <see cref="QuerySplittingBehavior"/>.
    /// </summary>
    public QuerySplittingBehavior? QuerySplittingBehavior { get; set; }

    /// <summary>
    /// Gets or sets the flag to use relation nulls.
    /// </summary>
    public bool? UseRelationalNulls { get; set; } = true;

    /// <summary>
    /// The minimum number of statements that are needed for a multi-statement command sent to the database during <c>SaveChanges()</c> or <c>null</c> if none has been set.
    /// </summary>
    public virtual int? MinBatchSize { get; set; }

    /// <summary>
    /// The maximum number of statements that will be included in commands sent to the database during <c>SaveChanges()</c> or <c>null</c> if none has been set.
    /// </summary>
    public virtual int? MaxBatchSize { get; set; }

    /// <inheritdoc />
    public override string ToString() =>
        $"{nameof(Timeout)}: {Timeout}, {nameof(Failure)}: {Failure}, {nameof(EnableSensitiveDataLogging)}: {EnableSensitiveDataLogging}, {nameof(EnableDetailedErrors)}: {EnableDetailedErrors}, {nameof(QuerySplittingBehavior)}: {QuerySplittingBehavior}, {nameof(UseRelationalNulls)}: {UseRelationalNulls}, {nameof(MinBatchSize)}: {MinBatchSize}, {nameof(MaxBatchSize)}: {MaxBatchSize}";
}