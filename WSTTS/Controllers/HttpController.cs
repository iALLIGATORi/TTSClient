using System.Net.Http;
using System.Threading.Tasks;

namespace Cloud
{
    internal class HttpController
    {
        internal async Task<HttpResponseMessage> Post(string requestUri, HttpContent content)
        {
            return await new HttpFactory().Client.PostAsync(requestUri, content);
        }

        internal async Task<string> Get(string requestUri)
        {
            return await new HttpFactory().Client.GetStringAsync(requestUri);
        }

        internal async Task Delete()
        {
            var requestUri = "https://cloud.speechpro.com/vktts/rest/session";
            await new HttpFactory().Client.DeleteAsync(requestUri);
        }
    }
}