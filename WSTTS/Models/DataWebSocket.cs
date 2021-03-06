using System.Text.Json.Serialization;

namespace Cloud
{
    internal class DataWebSocket
    {
        internal DataWebSocket(string voicename, WebSocketTextParam text)
        {
            VoiceName = voicename;
            Text = text;
            Audio = "audio/wav";
        }

        [JsonPropertyName("voice_name")] public string VoiceName { get; set; }

        [JsonPropertyName("text")] public WebSocketTextParam Text { get; set; }

        [JsonPropertyName("audio")] public string Audio { get; set; }
    }
}