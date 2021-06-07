using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cloud
{
    internal class LanguageController
    {
        private readonly string RequestUri = "https://cp.speechpro.com/vktts/rest/v1/languages";

        internal async Task<Languages> ToRequest(string sessionId)
        {
            new HttpFactory().Create(sessionId);
            var request = await new HttpController().Get(RequestUri);
            var languages = JsonSerializer.Deserialize<Languages[]>(request).OrderBy(lang => lang.Id);
            return new Selection().SelectLanguage(languages);
        }
    }
}