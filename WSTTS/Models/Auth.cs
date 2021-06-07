using System.Text.Json.Serialization;

namespace Cloud
{
    internal class Auth
    {
        [JsonPropertyName("session_id")] public string SessionId { get; set; }
    }
}