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
        public static async Task Packadge(string sessionId, Voices voiceRequest, string text)
        {
            var requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize";
            var synthesizeText = new SynthesizeText(text);
            try
            {
                var synthesizeContent = JsonContent.ToJsonContent(new PackageRequest(voiceRequest.Name, synthesizeText));

                var synthesizeResponse = await HttpClientFactory.Post(requestUri, synthesizeContent);

                var base64String = await synthesizeResponse.Content.ReadAsStringAsync();
                var base64 = JsonSerializer.Deserialize<Sound>(base64String);
                if (base64 == null)
                {
                    Console.WriteLine("Нет звуковых данных");
                }

                var sound = Convert.FromBase64String(base64.Data);

                SaveFile.Save(sound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public static async Task<Uri> Websocket(string sessionId, Voices voice, string text)
        {
            var requestUri = "https://cp.speechpro.com/vktts/rest/v1/synthesize/stream";
            var rate = "8000";
            var sampleRate = 22000;
            if (voice.Name.Contains(rate))
            {
                sampleRate = 8000;
            }


            var synthesizeWebText = new WebSocketTextParam();
            var synthesizeWebContent = JsonContent.ToJsonContent(new WebSocketRequest(voice.Name, synthesizeWebText));
            var synthesizeWebResponse = await HttpClientFactory.Post(requestUri, synthesizeWebContent);

            var urlString = synthesizeWebResponse.Content.ReadAsStringAsync().Result;
            var url = JsonSerializer.Deserialize<WebsocketUrl>(urlString);
            var uri = new Uri(url.Url);

            using (var apiWebsocketClient = new ClientWebSocket())
            {
                await apiWebsocketClient.ConnectAsync(uri, CancellationToken.None);

                //while (apiWebsocketClient.State == WebSocketState.Open)

                var messageByte = Encoding.UTF8.GetBytes(text);
                var bufferSend = new ArraySegment<byte>(messageByte);
                var bufferReceive = new ArraySegment<byte>(new byte[10240]);

                await apiWebsocketClient.SendAsync(bufferSend, WebSocketMessageType.Text, true,
                    CancellationToken.None);

                using (var ms = new MemoryStream())
                {
                    while (true)
                    {
                        var result = await apiWebsocketClient.ReceiveAsync(bufferReceive, CancellationToken.None);
                        await ms.WriteAsync(bufferReceive.Array, 0, result.Count, CancellationToken.None);
                        if (result.Count == 0)
                        {
                            break;
                        }
                    }

                    ms.Seek(0, SeekOrigin.Begin);
                    SaveFile.Save(ms, sampleRate);
                }

                Console.WriteLine("Синтез завершен");
                return uri;
            }
        }
    }
}