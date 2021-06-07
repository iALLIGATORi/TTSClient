using System.Net.Http;

namespace Cloud
{
    internal class HttpFactory : HttpClient
    {
        internal static readonly HttpClient Client = new HttpClient();

        internal static void Create(string sessionId)
        {
            Client.DefaultRequestHeaders.Add("X-Session-Id", sessionId);
        }
    }
}