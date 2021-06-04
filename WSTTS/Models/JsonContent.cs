using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CloudTTS
{
    internal class JsonContent
    {
        public static StringContent ToJsonContent(object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}