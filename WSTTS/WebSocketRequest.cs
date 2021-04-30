using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudTTS
{
    class WebSocketRequest
    {

        public WebSocketRequest(string voicename, WebSocketTextParam text)
        {
            VoiceName = voicename;
            Text = text;
            Audio = "audio/wav";
        }

        [JsonPropertyName("voice_name")]
        public string VoiceName { get; set; }

        [JsonPropertyName("text")]
        public WebSocketTextParam Text { get; set; }

        [JsonPropertyName("audio")]
        public string Audio { get; set; }

        public StringContent ToJsonContent()
        {
            var json = JsonSerializer.Serialize(this);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
