using System.Text.Json.Serialization;

namespace Cloud
{
    public class Auth
    {
        [JsonPropertyName("session_id")] public string SessionId { get; set; }
    }
}