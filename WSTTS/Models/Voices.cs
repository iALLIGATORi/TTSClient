using System.Text.Json.Serialization;

namespace CloudTTS
{
    public class Voices
    {
        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("gender")] public string Gender { get; set; }
    }
}