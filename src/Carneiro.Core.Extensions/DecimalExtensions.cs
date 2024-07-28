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
    public static string ToQuantityFormat(this decimal amount) => amount.ToString("N2");

    /// <summary>
    /// Converts a <see cref="decimal"/> into a two decimal places string.
    /// </summary>
    /// <param name="decimal"></param>
    public static string ToPrice(this decimal? @decimal) => @decimal.HasValue ? @decimal.ToPrice() : null;

    /// <summary>
    /// Converts a <see cref="decimal"/> into a two decimal places string.
    /// </summary>
    /// <param name="decimal"></param>
    public static string ToPrice(this decimal @decimal) => @decimal.ToString("N2");
}