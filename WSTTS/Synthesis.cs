using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class Synthesis
    {
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

        internal static async Task SynthesizingPackage(string sessionId, Voices voiceRequest, string text)
        {
            var requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize";
            var packageParam = new PackageTextParam(text);
            var packageContent = JsonContent.ToJsonContent(new DataPackage(voiceRequest.Name, packageParam));

            var packageResponse = await HttpRequest.Post(requestUri, packageContent);
            
            var base64Json = await packageResponse.Content.ReadAsStringAsync();
            SoundConverter.Base64ToSound(base64Json);
        }


        internal static async Task<Uri> SynthesizingWebsocket(string sessionId, Voices voice, string text)
        {
            var requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize/stream";
            var rate = "8000";
            var sampleRate = 22000;
            if (voice.Name.Contains(rate))
            {
                sampleRate = 8000;
            }

            var webParam = new WebSocketTextParam();
            var webContent = JsonContent.ToJsonContent(new DataWebSocket(voice.Name, webParam));
            var webResponse = await HttpRequest.Post(requestUri, webContent);

            var urlString = webResponse.Content.ReadAsStringAsync().Result;
            var url = JsonSerializer.Deserialize<WebsocketUrl>(urlString);
            var uri = new Uri(url.Url);
            var webClient = await WebSocketClient.Connect(uri);

            // TODO: сделать проверку статуса вебсокета и исключений
            //while (apiWebsocketClient.State == WebSocketState.Open)

            var messageByte = Encoding.UTF8.GetBytes(text);
            var bufferSend = new ArraySegment<byte>(messageByte);
            var bufferReceive = new ArraySegment<byte>(new byte[8192]);

            await webClient.SendAsync(bufferSend, WebSocketMessageType.Text, true, CancellationToken.None);


            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    var result = await webClient.ReceiveAsync(bufferReceive, CancellationToken.None);
                    await ms.WriteAsync(bufferReceive.Array, 0, result.Count, CancellationToken.None);
                    if ((result.Count == 0) & result.EndOfMessage)
                    {
                        break;
                    }
                }

                ms.Seek(0, SeekOrigin.Begin);
                FileSaver.Save(ms, sampleRate);
            }

            Console.WriteLine("Синтез завершен");
            return uri;
        }
    }
}