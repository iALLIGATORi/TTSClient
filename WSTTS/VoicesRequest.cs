using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class VoicesRequest
    {
        internal static async Task<Voices> Request(string sessionId, Languages language)
        {
            var requestUri = "https://cloud.speechpro.com/vktts/rest/v1/languages/";
            var voicesString = await HttpRequest.Get(requestUri + language.Name + "/voices");
            var voices = JsonSerializer.Deserialize<Voices[]>(voicesString).OrderBy(voice => voice.Name);
            return Selection.SelectVoice(voices);
        }
    }
}