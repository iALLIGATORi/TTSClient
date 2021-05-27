using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudTTS
{
    public class PackageRequest
    {
        public PackageRequest(string voiceName, SynthesizeText text)
        {
            VoiceName = voiceName;
            Text = text;
            Audio = "audio/wav";
        }

        [JsonPropertyName("voice_name")] public string VoiceName { get; set; }

        [JsonPropertyName("text")] public SynthesizeText Text { get; set; }

        [JsonPropertyName("audio")] public string Audio { get; set; }

    }
}