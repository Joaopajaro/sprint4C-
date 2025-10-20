using System.Text.Json.Serialization;

namespace StudentApi.Models
{
   
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
