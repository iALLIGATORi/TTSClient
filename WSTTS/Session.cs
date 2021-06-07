using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cloud
{
    internal class Session
    {
        public async Task<string> Create(Credentials credentials)
        {
            var requestUri = "https://cp.speechpro.com/vksession/rest/session";
            var credentialsContent = new JsonContent().ToJson(credentials);
            var createSession = await new HttpController().Post(requestUri, credentialsContent);
            var session = await createSession.Content.ReadAsStringAsync();
            var sessionId = JsonSerializer.Deserialize<Auth>(session);
            if (sessionId?.SessionId == null)
            {
                throw new ArgumentNullException();
            }

            return sessionId.SessionId;
        }

        // TODO: проверка статуса сессии
        public async Task<bool> Status(Task<string> sessionId)
        {
            var requestUri = "https://cloud.speechpro.com/vksession/rest/session";
            var statusString = await new HttpController().Get(requestUri);
            var status = JsonSerializer.Deserialize<SessionStatus>(statusString);
            if (status.Active == false)
            {
                throw new ArgumentNullException();
            }

            return status.Active;
        }
    }
}