using System.Text.Json.Serialization;

namespace Cloud
{
    internal class SessionStatus
    {
        [JsonPropertyName("is_active")] public bool Active { get; set; }
    }
}