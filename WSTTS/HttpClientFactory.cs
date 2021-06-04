using System.Net.Http;

namespace CloudTTS
{
    internal class HttpClientFactory : HttpClient
    {
        internal static readonly HttpClient Client = new HttpClient();

        internal static void Create(string sessionId)
        {
            Client.DefaultRequestHeaders.Add("X-Session-Id", sessionId);
        }
    }
}