using System;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CloudTTS;
using NAudio.Wave;

namespace WSTTS
{
    internal class SynthesisWebsocket
    {
        public static async Task<Uri> Synthesize(Task<string> sessionId, Task<Voices> voice)
        {
            var rate = "8000";
            var sampleRate = 22000;
            if (voice.Result.Name.Contains(rate))
            {
                sampleRate = 8000;
            }

            using (var apiClient = new HttpClient())
            {
                apiClient.DefaultRequestHeaders.Add("X-Session-Id", sessionId.Result);
                var synthesizeWebText = new WebSocketTextParam();
                var synthesizeWebContent = new WebSocketRequest(voice.Result.Name, synthesizeWebText).ToJsonContent();
                var synthesizeWebResponse = await apiClient.PostAsync(
                    "https://cp.speechpro.com/vktts/rest/v1/synthesize/stream",
                    synthesizeWebContent);
                var urlString = synthesizeWebResponse.Content.ReadAsStringAsync().Result;
                var url = JsonSerializer.Deserialize<WebsocketUrl>(urlString);
                var uri = new Uri(url.Url);

                using (var apiWebsocketClient = new ClientWebSocket())
                {
                    await apiWebsocketClient.ConnectAsync(uri, CancellationToken.None);
                    Console.WriteLine("Введите текст для синтеза");

                    //while (apiWebsocketClient.State == WebSocketState.Open)

                    var message = "В какой-то момент бедных стало слишком много.";
                    var messageByte = Encoding.UTF8.GetBytes(message);
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

                        Console.WriteLine("Создаем файл");

                        var dir = new DirectoryInfo(@"C:\Cloud\");
                        if (!dir.Exists)
                        {
                            dir.Create();
                        }
                        var wavName = "websocket.wav";
                        var pathFile = dir + wavName;

                        var pcmToWave = new RawSourceWaveStream(ms, new WaveFormat(sampleRate, 1));
                        WaveFileWriter.CreateWaveFile(pathFile, pcmToWave);
                        Console.WriteLine("Файл " + wavName + " размером " + ms.Length +
                                          " байт сохранен в папке " + dir);
                    }

                    Console.WriteLine("Синтез завершен");
                    return uri;
                }
            }
        }
    }
}