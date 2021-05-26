using System.Text.Json.Serialization;

namespace CloudTTS
{
    public class WebSocketTextParam
    {
        public WebSocketTextParam()
        {
            Mime = "text/plain";
        }

        [JsonPropertyName("mime")] public string Mime { get; set; }
    }
}