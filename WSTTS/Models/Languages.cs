using System.Text.Json.Serialization;

namespace Cloud
{
    internal class Languages
    {
        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }
    }
}