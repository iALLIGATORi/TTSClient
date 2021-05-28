using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudTTS
{
    public class DataPackage
    {
        public DataPackage(string voiceName, PackageTextParam text)
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