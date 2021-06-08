using System.Text;
using System.Threading.Tasks;

namespace Cloud
{
    internal class Synthesis
    {
        private string _requestUri;

        internal async Task Synthesizing(string session, Voices voice, int keyMode, string text)
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

        private async Task SynthesizingPackage(string sessionId, Voices voice, string text)
        {
            _requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize";

            var packageContent = new JsonContent().ToJson(new RequestController().RequestPackage(voice, text));
            var packageResponse = await new HttpController().Post(_requestUri, packageContent);
            var base64 = await packageResponse.Content.ReadAsStringAsync();
            var sound = new SoundConverter().Base64ToSound(base64);
            new FileSaver().Save(sound);
        }


        private async Task SynthesizingWebsocket(string sessionId, Voices voice, string text)
        {
            _requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize/stream";
            var sampleRate = new SampleRate().SamplingRate(voice);
            var webContent = new JsonContent().ToJson(new RequestController().RequestWebsocket(voice));
            var webResponse = await new HttpController().Post(_requestUri, webContent);
            var urlJson = webResponse.Content.ReadAsStringAsync().Result;
            var uri = new UrlConverter().ConvertingToUri(urlJson);
            await WebSocketController.Connect(uri);

            // TODO: сделать проверку статуса вебсокета и исключений
            //while (apiWebsocketClient.State == WebSocketState.Open)

            var messageByte = Encoding.UTF8.GetBytes(text);
            await WebSocketController.Send(messageByte);
            await WebSocketController.Receive(sampleRate);
        }
    }
}