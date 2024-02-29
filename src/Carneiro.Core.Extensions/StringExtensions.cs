namespace Carneiro.Core.Extensions;

/// <summary>
/// <see cref="string"/> Extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Removes the duplicated white spaces.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static string RemoveDuplicatedWhiteSpaces(this string str) => Regex.Replace(str, @"\s+", " ");

    /// <summary>
    /// Generates the slug.
    /// </summary>
    /// <param name="phrase">The phrase.</param>
    /// <returns></returns>
    public static string GenerateSlug(this string phrase)
    {
        var str = RemoveAccent(phrase).ToLower(culture: CultureInfo.DefaultThreadCurrentUICulture);
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        // convert multiple spaces into one space 
        str = Regex.Replace(str, @"\s+", " ").Trim();
        // cut and trim 
        str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
        str = Regex.Replace(str, @"\s", "-");
        do
        {
            str = str.Replace("--", "-");
        } while (str.Contains("--"));


        while (str[^1] == '-')
        {
            str = str.Remove(str.Length - 1);
        }

        return str;
    }

    static readonly string[] s_pats3 = { "é", "É", "á", "Á", "í", "Í", "ó", "Ó", "ú", "Ú", "à", "À" };
    static readonly string[] s_repl3 = { "e", "E", "a", "A", "i", "I", "o", "O", "u", "U", "a", "A" };
    static Dictionary<string, string> s_var;
    static Dictionary<string, string> Dict
    {
        get
        {
            return s_var ??= s_pats3.Zip(s_repl3, (k, v) => new { Key = k, Value = v }).ToDictionary(o => o.Key, o => o.Value);
        }
    }

    private static string RemoveAccent(string text)
    {
        var pattern = string.Join("|", Dict.Keys.Select(k => k));
        var result = Regex.Replace(text, pattern, m => Dict[m.Value]);

        return result;
    }
}