namespace Carneiro.Core.Extensions;

/// <summary>
/// Extensions for <see cref="decimal"/>.
/// </summary>
public static class DecimalExtensions
{
    /// <summary>
    /// Converts a <see cref="decimal"/> into a two decimal places string.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns></returns>
    public static string ToQuantityFormat(this decimal amount) => amount.ToString("N2");
}