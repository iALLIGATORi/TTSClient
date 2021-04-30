using System.Text.Json.Serialization;

namespace CloudTTS
{
    internal class Sound
    {
        [JsonPropertyName("data")] 
        public string Data { get; set; }
    }
}