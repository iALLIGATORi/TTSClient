using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTTS
{
    public  class DataRequest
    {
        internal static object ToRequest(Voices voice)
        {
            var webParam = new WebSocketTextParam();
            return new DataWebSocket(voice.Name, webParam);
        }
        internal static object ToRequest(Voices voice, string text)
        {
            var packageParam = new PackageTextParam(text);
            return new DataPackage(voice.Name, packageParam);
        }
    }
}
