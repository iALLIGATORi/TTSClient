using System.Text.Json.Serialization;

namespace Cloud
{
    internal class Sound
    {
        [JsonPropertyName("data")] public string Data { get; set; }
    }
}