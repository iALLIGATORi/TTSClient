using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cloud
{
    internal static class VoiceController
    {
        private static readonly string RequestUrl = "https://cloud.speechpro.com/vktts/rest/v1/languages/";

        internal static async Task<Voices> ToRequest(string sessionId, Languages language)
        {
            var httpController = new HttpController().Get(RequestUrl + language.Name + "/voices");
            var voicesString = await httpController;
            var voices = JsonSerializer.Deserialize<Voices[]>(voicesString).OrderBy(voice => voice.Name);
            return new Selection().SelectVoice(voices);
        }
    }
}