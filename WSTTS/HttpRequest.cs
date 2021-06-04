using System.Net.Http;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class HttpRequest
    {
        public static Task<HttpResponseMessage> Post(string requestUri, HttpContent content)
        {
            var client = HttpClientFactory.Client;
            return client.PostAsync(requestUri, content);
        }

        internal static Task<string> Get(string requestUri)
        {
            var client = HttpClientFactory.Client;
            return client.GetStringAsync(requestUri);
        }
    }
}