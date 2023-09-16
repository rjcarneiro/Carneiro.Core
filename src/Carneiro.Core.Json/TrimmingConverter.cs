using Newtonsoft.Json;

namespace Carneiro.Core.Json;

/// <summary>
/// Trims all strings that comes from the http request.
/// </summary>
/// <seealso cref="Newtonsoft.Json.JsonConverter" />
public class TrimmingConverter : JsonConverter
{
    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>.
    /// </value>
    public override bool CanRead => true;

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.
    /// </value>
    public override bool CanWrite => false;

    /// <summary>
    /// Determines whether this instance can convert the specified object type.
    /// </summary>
    /// <param name="objectType">Type of the object.</param>
    /// <returns>
    /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanConvert(Type objectType) => objectType == typeof(string);

    /// <summary>
    /// Reads the JSON representation of the object.
    /// </summary>
    /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="existingValue">The existing value of object being read.</param>
    /// <param name="serializer">The calling serializer.</param>
    /// <returns>
    /// The object value.
    /// </returns>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => ((string)reader.Value)?.Trim();

    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
    /// <param name="value">The value.</param>
    /// <param name="serializer">The calling serializer.</param>
    /// <exception cref="NotImplementedException"></exception>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
}