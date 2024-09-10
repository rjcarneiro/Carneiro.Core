using System.Text;

namespace Carneiro.Core.Repository;

/// <summary>
/// Class that represents the result of a stored procedure.
/// </summary>
/// <typeparam name="T"></typeparam>
public class StoreProcedureResult<T> where T : class
{
    /// <summary>
    /// Gets the result.
    /// </summary>
    public T Result { get; init; }

    /// <summary>
    /// Gets the output.
    /// </summary>
    public Dictionary<string, object> Output { get; init; }

    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder($"Output = {Output.Count}");

        foreach (KeyValuePair<string, object> entry in Output)
        {
            sb.AppendLine($"Key = {entry.Key} Value = {entry.Value}");
        }

        return sb.ToString();
    }
}