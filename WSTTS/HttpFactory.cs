using System.Net.Http;

namespace Cloud
{
    internal class HttpFactory
    {
        internal readonly HttpClient Client = new HttpClient();

        internal void Create(string sessionId)
        {
            Client.DefaultRequestHeaders.Add("X-Session-Id", sessionId);
        }
    }
}