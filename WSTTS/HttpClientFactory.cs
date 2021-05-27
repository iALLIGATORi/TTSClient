using System.Net.Http;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class HttpClientFactory : HttpClient
    {
        public static readonly HttpClient HttpClient = new HttpClient();

        public static void Create(string sessionId)
        {
            HttpClient.DefaultRequestHeaders.Add("X-Session-Id", sessionId);
        }

        public static Task<HttpResponseMessage> Post(string requestUri, HttpContent content)
        {
            return HttpClient.PostAsync(requestUri, content);
        }

        public static Task<string> Get(string requestUri)
        {
            return HttpClient.GetStringAsync(requestUri);
        }
    }
}