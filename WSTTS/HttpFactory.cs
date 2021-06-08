using System.Net.Http;

namespace Cloud
{
    internal static class HttpFactory
    {
        internal static readonly HttpClient Client = new HttpClient();

        internal static void Create(string sessionId)
        {
            Client.DefaultRequestHeaders.Add("X-Session-Id", sessionId);
        }
    }
}