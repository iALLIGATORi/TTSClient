using System.Text.Json.Serialization;

namespace CloudTTS
{
    internal class WebsocketUrl
    {
        [JsonPropertyName("url")] public string Url { get; set; }
    }
}