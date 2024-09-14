using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Carneiro.Core.Json;

/// <summary>
/// Class that brings Json serialization/deserialization helper methods.
/// </summary>
public static class JsonHelper
{
    private static JsonSerializerSettings _jsonSettings;

    /// <summary>
    /// Gets the json settings.
    /// </summary>
    /// <value>
    /// The json settings.
    /// </value>
    public static JsonSerializerSettings JsonSettings
    {
        get
        {
            if (_jsonSettings != null)
                return _jsonSettings;
            
            _jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.DateTime,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };
            _jsonSettings.Converters.Add(new TrimmingConverter());
            _jsonSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"
            });
            return _jsonSettings;
        }
    }

    /// <summary>
    /// Serializes the specified input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input">The input.</param>
    public static string Serialize<T>(T input) => JsonConvert.SerializeObject(input, JsonSettings);

    /// <summary>
    /// Serializes the specified input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input">The input.</param>
    /// <param name="action">The action.</param>
    /// <exception cref="System.ArgumentNullException">action</exception>
    public static string Serialize<T>(T input, Action<JsonSerializerSettings> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        JsonSerializerSettings settings = JsonSettings;
        action(settings);
        return JsonConvert.SerializeObject(input, settings);
    }

    /// <summary>
    /// Serializes the specified input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input">The input.</param>
    /// <param name="settings">The settings.</param>
    public static string Serialize<T>(T input, JsonSerializerSettings settings) => JsonConvert.SerializeObject(input, settings);

    /// <summary>
    /// Deserializes the specified input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input">The input.</param>
    public static T Deserialize<T>(string input) => JsonConvert.DeserializeObject<T>(input, JsonSettings);

    /// <summary>
    /// Deserializes the specified input asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input">The input.</param>
    public static Task<T> DeserializeAsync<T>(string input) => Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(input, JsonSettings));

    /// <summary>
    /// Deserializes the specified input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input">The input.</param>
    /// <param name="settings">The settings.</param>
    public static T Deserialize<T>(string input, JsonSerializerSettings settings) => JsonConvert.DeserializeObject<T>(input, settings);

    /// <summary>
    /// Deserializes the specified input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input">The input.</param>
    /// <param name="action">The action.</param>
    /// <exception cref="System.ArgumentNullException">action</exception>
    public static T Deserialize<T>(string input, Action<JsonSerializerSettings> action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));
        JsonSerializerSettings settings = JsonSettings;
        action(settings);
        return JsonConvert.DeserializeObject<T>(input, settings);
    }

    /// <summary>
    /// Deserializes the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="type">The type.</param>
    public static object Deserialize(string input, Type type) => Deserialize(input, type, JsonSettings);

    /// <summary>
    /// Deserializes the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="type">The type.</param>
    /// <param name="settings">The settings.</param>
    public static object Deserialize(string input, Type type, JsonSerializerSettings settings) => JsonConvert.DeserializeObject(input, type, settings);
}