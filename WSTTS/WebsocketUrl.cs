using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudTTS
{
    class WebsocketUrl
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
