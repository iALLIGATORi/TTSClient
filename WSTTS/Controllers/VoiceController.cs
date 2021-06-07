using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cloud
{
    internal class VoiceController
    {
        private static readonly string RequestUrl = "https://cloud.speechpro.com/vktts/rest/v1/languages/";

        internal static async Task<Voices> ToRequest(string sessionId, Languages language)
        {
            var voicesString = await HttpController.Get(RequestUrl + language.Name + "/voices");
            var voices = JsonSerializer.Deserialize<Voices[]>(voicesString).OrderBy(voice => voice.Name);
            return Selection.SelectVoice(voices);
        }
    }
}