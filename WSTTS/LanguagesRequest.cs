using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class LanguagesRequest
    {
        internal static async Task<Languages> ToRequest(string sessionId)
        {
            var requestUri = "https://cp.speechpro.com/vktts/rest/v1/languages";
            HttpClientFactory.Create(sessionId);
            var request = await HttpRequest.Get(requestUri);
            var languages = JsonSerializer.Deserialize<Languages[]>(request).OrderBy(lang => lang.Id);
            return Selection.SelectLanguage(languages);
        }
    }
}