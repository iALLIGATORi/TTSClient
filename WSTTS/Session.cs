using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class Session
    {
        public static async Task<string> Create(Credentials credentials)
        {
            var requestUri = "https://cp.speechpro.com/vksession/rest/session";
            var credentialsContent = JsonContent.ToJsonContent(credentials);
            try
            {
                var createSession = await HttpClientFactory.Post(requestUri, credentialsContent);
                var sessionIdString = await createSession.Content.ReadAsStringAsync();
                var sessionId = JsonSerializer.Deserialize<Auth>(sessionIdString);
                if (sessionId?.SessionId == null)
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

        public static async Task<bool> Status(Task<string> sessionId)
        {
            var requestUri = "https://cloud.speechpro.com/vksession/rest/session";
            var statusString = await HttpClientFactory.Get(requestUri);
            var status = JsonSerializer.Deserialize<SessionStatus>(statusString);
            if (status.Active == false)
            {
                throw new ArgumentNullException();
            }

            return status.Active;
        }
    }
}