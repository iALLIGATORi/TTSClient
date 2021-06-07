using System.Text.Json.Serialization;

namespace Cloud
{
    internal class WebsocketUrl
    {
        [JsonPropertyName("url")] public string Url { get; set; }
    }
}