using System.Text.Json.Serialization;

namespace CloudTTS
{
    public class SynthesizeText
    {
        public SynthesizeText()
        {
            Mime = "text/plain";
        }

        public SynthesizeText(string value)
        {
            Mime = "text/plain";
            Value = value;
        }

        [JsonPropertyName("mime")] public string Mime { get; set; }

        [JsonPropertyName("value")] public string Value { get; set; }
    }
}