using System.Text;
using System.Threading.Tasks;

namespace Cloud
{
    internal class Synthesis
    {
        private static string _requestUri;

        internal static async Task Synthesizing(string session, Voices voice, int keyMode, string text)
        {
            if (keyMode == 1)
            {
                await SynthesizingPackage(session, voice, text);
            }
            else
            {
                await SynthesizingWebsocket(session, voice, text);
            }
        }

        protected static async Task SynthesizingPackage(string sessionId, Voices voice, string text)
        {
            _requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize";

            var packageContent = JsonContent.ToJson(RequestController.RequestPackage(voice, text));
            var packageResponse = await HttpController.Post(_requestUri, packageContent);
            var base64 = await packageResponse.Content.ReadAsStringAsync();
            var sound = SoundConverter.Base64ToSound(base64);
            FileSaver.Save(sound);
        }


        protected static async Task SynthesizingWebsocket(string sessionId, Voices voice, string text)
        {
            _requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize/stream";
            var sampleRate = SampleRate.SamplingRate(voice);
            var webContent = JsonContent.ToJson(RequestController.RequestWebsocket(voice));
            var webResponse = await HttpController.Post(_requestUri, webContent);
            var urlJson = webResponse.Content.ReadAsStringAsync().Result;
            var uri = UrlConverter.ConvertingToUri(urlJson);
            await WebSocketController.Connect(uri);

            // TODO: сделать проверку статуса вебсокета и исключений
            //while (apiWebsocketClient.State == WebSocketState.Open)

            var messageByte = Encoding.UTF8.GetBytes(text);
            await WebSocketController.Send(messageByte);
            await WebSocketController.Receive(sampleRate);
        }
    }
}