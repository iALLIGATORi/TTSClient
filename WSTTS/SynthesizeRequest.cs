using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudTTS
{
    public class SynthesizeRequest
    {
        public SynthesizeRequest(string voicename, SynthesizeText text)
        {
            VoiceName = voicename;
            Text = text;
            Audio = "audio/wav";
        }

        [JsonPropertyName("voice_name")] 
        public string VoiceName { get; set; }

        [JsonPropertyName("text")] 
        public SynthesizeText Text { get; set; }

        [JsonPropertyName("audio")] 
        public string Audio { get; set; }

        public StringContent ToJsonContent()
        {
            var json = JsonSerializer.Serialize(this);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}