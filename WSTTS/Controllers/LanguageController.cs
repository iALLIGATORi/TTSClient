using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cloud
{
    internal class LanguageController
    {
        private static readonly string RequestUri = "https://cp.speechpro.com/vktts/rest/v1/languages";

        internal static async Task<Languages> ToRequest(string sessionId)
        {
            HttpFactory.Create(sessionId);
            var request = await HttpController.Get(RequestUri);
            var languages = JsonSerializer.Deserialize<Languages[]>(request).OrderBy(lang => lang.Id);
            return Selection.SelectLanguage(languages);
        }
    }
}