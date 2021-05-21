using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class Session
    {
        [JsonPropertyName("is_active")]
        public bool Active { get; set; }

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
                    if (sessionId.SessionId == null)
                    {
                        throw new ArgumentNullException();
                    }

                    return sessionId.SessionId;
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async Task<bool> Status(Task<string> sessionId)
        {
            using (var apiClient = new HttpClient())
            {
                apiClient.DefaultRequestHeaders.Add("X-Session-Id", sessionId.Result);
                var statusString = await apiClient.GetStringAsync("https://cloud.speechpro.com/vksession/rest/session");
                var status = JsonSerializer.Deserialize<Session>(statusString);
                if (status.Active == false)
                {
                    throw new ArgumentNullException();
                }
                return status.Active;
            }

        }
    }
}