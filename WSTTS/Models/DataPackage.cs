using System.Text.Json.Serialization;

namespace Cloud
{
    internal class DataPackage
    {
        internal DataPackage(string voiceName, PackageTextParam text)
        {
            VoiceName = voiceName;
            Text = text;
            Audio = "audio/wav";
        }

        [JsonPropertyName("voice_name")] public string VoiceName { get; set; }

        [JsonPropertyName("text")] public PackageTextParam Text { get; set; }

        [JsonPropertyName("audio")] public string Audio { get; set; }
    }
}