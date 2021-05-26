using System.Text.Json.Serialization;

namespace CloudTTS
{
    public class Auth
    {
        [JsonPropertyName("session_id")] public string SessionId { get; set; }
    }
}