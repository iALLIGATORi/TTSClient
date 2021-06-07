using System.Text.Json.Serialization;

namespace Cloud
{
    internal class WebSocketTextParam
    {
        internal WebSocketTextParam()
        {
            Mime = "text/plain";
        }

        [JsonPropertyName("mime")] public string Mime { get; set; }
    }
}