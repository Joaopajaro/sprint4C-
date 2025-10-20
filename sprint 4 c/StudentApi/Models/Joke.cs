using System.Text.Json.Serialization;

namespace StudentApi.Models
{
    /// <summary>
    /// Represents a joke returned by the external joke API.  The property names
    /// correspond to the JSON returned by the API.  JsonPropertyName is used to
    /// map lowercase JSON keys to PascalCase C# properties.
    /// </summary>
    public class Joke
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("setup")]
        public string Setup { get; set; } = string.Empty;

        [JsonPropertyName("punchline")]
        public string Punchline { get; set; } = string.Empty;
    }
}