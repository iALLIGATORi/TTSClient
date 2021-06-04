using System.Text;
using System.Threading.Tasks;

namespace CloudTTS
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

        internal static async Task SynthesizingPackage(string sessionId, Voices voice, string text)
        {
            _requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize";

            var packageContent = Content.ToJson(DataRequest.ToRequest(voice, text));
            var packageResponse = await HttpRequest.Post(_requestUri, packageContent);
            var base64 = await packageResponse.Content.ReadAsStringAsync();
            var sound = SoundConverter.Base64ToSound(base64);
            FileSaver.Save(sound);
        }


        internal static async Task SynthesizingWebsocket(string sessionId, Voices voice, string text)
        {
            _requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize/stream";
            var sampleRate = SampleRate.SamplingRate(voice);
            var webContent = Content.ToJson(DataRequest.ToRequest(voice));
            var webResponse = await HttpRequest.Post(_requestUri, webContent);

            var urlJson = webResponse.Content.ReadAsStringAsync().Result;
            var uri = UrlConverter.ConvertingToUri(urlJson);
            await WebSocketClient.Connect(uri);

            // TODO: сделать проверку статуса вебсокета и исключений
            //while (apiWebsocketClient.State == WebSocketState.Open)

            var messageByte = Encoding.UTF8.GetBytes(text);
            await WebSocketClient.Send(messageByte);
            await WebSocketClient.Receive(sampleRate);
        }
    }
}