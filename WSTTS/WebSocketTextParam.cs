using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudTTS
{
    public class WebSocketTextParam
    {
        public WebSocketTextParam()
        {
            Mime = "text/plain";
        }

        [JsonPropertyName("mime")]
        public string Mime { get; set; }

    }
}
