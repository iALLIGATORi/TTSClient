using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class HttpClientFactory : HttpClient
    {
        internal static readonly HttpClient Client = new HttpClient();
        public static void Create(string sessionId)
        {
            Client.DefaultRequestHeaders.Add("X-Session-Id", sessionId);
        }
    }
}