namespace Carneiro.Core.Tests;

/// <summary>
/// Enum that sets how a scenario should be initialized.
/// </summary>
public enum ScenarioType
{
    /// <summary>
    /// The Sql Server
    /// </summary>
    SqlServer = 0,

    /// <summary>
    /// The memory
    /// </summary>
    InMemory = 1,

    /// <summary>
    /// The SQL lite
    /// </summary>
    SqlLite = 2,

    /// <summary>
    /// The SQL lite in memory
    /// </summary>
    SqlLiteInMemory = 3,

    /// <summary>
    /// The no data
    /// </summary>
    NoData = 4
}