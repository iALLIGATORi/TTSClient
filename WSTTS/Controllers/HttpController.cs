using System.Net.Http;
using System.Threading.Tasks;

namespace Cloud
{
    internal class HttpController
    {
        internal static async Task<HttpResponseMessage> Post(string requestUri, HttpContent content)
        {
            var client = HttpFactory.Client;
            return await client.PostAsync(requestUri, content);
        }

        internal static async Task<string> Get(string requestUri)
        {
            var client = HttpFactory.Client;
            return await client.GetStringAsync(requestUri);
        }

        internal static async Task Delete()
        {
            var requestUri = "https://cloud.speechpro.com/vktts/rest/session";
            var client = HttpFactory.Client;
            await client.DeleteAsync(requestUri);
        }
    }
}