using System.Text.Json.Serialization;

namespace CloudTTS
{
    internal class SessionStatus
    {
        [JsonPropertyName("is_active")] public bool Active { get; set; }
    }
}