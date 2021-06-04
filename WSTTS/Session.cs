using System;
using System.Text.Json;
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
                var createSession = await HttpRequest.Post(requestUri, credentialsContent);
                var session = await createSession.Content.ReadAsStringAsync();
                var sessionId = JsonSerializer.Deserialize<Auth>(session);
                if (sessionId?.SessionId == null)
                {
                    throw new ArgumentNullException();
                }

                //createSession.Dispose();
                return sessionId.SessionId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // TODO: проверка статуса сессии
        public static async Task<bool> Status(Task<string> sessionId)
        {
            var requestUri = "https://cloud.speechpro.com/vksession/rest/session";
            var statusString = await HttpRequest.Get(requestUri);
            var status = JsonSerializer.Deserialize<SessionStatus>(statusString);
            if (status.Active == false)
            {
                throw new ArgumentNullException();
            }

            return status.Active;
        }
    }
}