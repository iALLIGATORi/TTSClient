using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal static class Session
    {
        public static async Task<string> Create(Credentials credentials)
        {
            using (var apiClient = new HttpClient())
            {
                var credentialsContent = credentials.ToJsonContent();
                try
                {
                    var createSession = await apiClient.PostAsync("https://cp.speechpro.com/vksession/rest/session",
                        credentialsContent);
                    var sessionIdString = await createSession.Content.ReadAsStringAsync();
                    var sessionId = JsonSerializer.Deserialize<Auth>(sessionIdString);
                    return sessionId.SessionId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}