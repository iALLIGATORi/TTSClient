using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    
    class SynthesisWebsocket
    {
        public static MemoryStream FileData;
        public static async Task Synthesize(Task<string> sessionId)
        {
            using (var apiClient = new HttpClient())
            {
                apiClient.DefaultRequestHeaders.Add("X-Session-Id", sessionId.Result);
                var synthesizeWebText = new WebSocketTextParam();
                var synthesizeWebContent = new WebSocketRequest("Alexander8000", synthesizeWebText).ToJsonContent();
                var synthesizeWebResponse = await apiClient.PostAsync("https://cp.speechpro.com/vktts/rest/v1/synthesize/stream",
                    synthesizeWebContent);
                var urlString = synthesizeWebResponse.Content.ReadAsStringAsync().Result;
                var url = JsonSerializer.Deserialize<WebsocketUrl>(urlString);
                var uri = new Uri(url.Url);

                using (var apiWebsocketClient = new ClientWebSocket())
                {
                    await apiWebsocketClient.ConnectAsync(uri, CancellationToken.None);
                    Console.WriteLine("Введите текст для синтеза");
                    //while (apiWebsocketClient.State == WebSocketState.Open)
                    //{
                        var message = "привет как дела раз два три";
                        var messageByte = Encoding.UTF8.GetBytes(message);
                        var bufferSend = new ArraySegment<byte>(messageByte);
                        var bufferReceive = new ArraySegment<byte>(new byte[6553]);

                        await apiWebsocketClient.SendAsync(bufferSend, WebSocketMessageType.Text, true, CancellationToken.None);

                        var dataWeb = await apiWebsocketClient.ReceiveAsync(bufferReceive, CancellationToken.None);


                    var bufferStream = new BufferedWaveProvider(new WaveFormat(8000, 16, 1));
                    //добавляем данные в буфер, откуда output будет воспроизводить звук
                    bufferStream.AddSamples(bufferReceive.Array, 0, dataWeb.Count);
                    //var savingWaveProvider = new SavingWaveProvider(bufferStream, @"C:\WSTTS\Webtts.wav");
                    //var buffer = new byte[65535];
                    //WaveFileWriter.CreateWaveFile(@"C:\WSTTS\Webtts333.wav", bufferStream);

                    




                    MemoryStream newFile = new MemoryStream(48 + dataWeb.Count);
                    newFile.Write(Encoding.UTF8.GetBytes("RIFF"), 0, 4); //Содержит символы "RIFF" в ASCII кодировке
                    newFile.Write(BitConverter.GetBytes(dataWeb.Count + 40), 0, 4); //Это оставшийся размер цепочки, начиная с этой позиции. Иначе говоря, это размер файла - 8
                    newFile.Write(Encoding.UTF8.GetBytes("WAVE"), 0, 4); //     Содержит символы "WAVE" 
                    newFile.Write(Encoding.UTF8.GetBytes("fmt "), 0, 4); //     Содержит символы "fmt "
                    newFile.Write(BitConverter.GetBytes(18), 0, 4); //длинна заголовка fmt - 18 для формата PCM
                    newFile.Write(BitConverter.GetBytes(1), 0, 2); //Для PCM = 1.Значения, отличающиеся от 1, обозначают некоторый формат сжатия.
                    newFile.Write(BitConverter.GetBytes(1), 0, 2); //Количество каналов. Моно = 1, Стерео = 2 и т.д.
                    newFile.Write(BitConverter.GetBytes(8000), 0, 4); //Частота дискретизации. 8000 Гц, 44100 Гц и т.д.
                    newFile.Write(BitConverter.GetBytes(8000), 0, 4); //Количество байт, переданных за секунду воспроизведения.
                    newFile.Write(BitConverter.GetBytes(1), 0, 2); //Количество байт для одного сэмпла, включая все каналы.
                    newFile.Write(BitConverter.GetBytes(16), 0, 4); //Количество бит в сэмпле. Так называемая "глубина" или точность звучания. 8 бит, 16 бит и т.д.
                    newFile.Write(Encoding.UTF8.GetBytes("data"), 0, 4); //Содержит символы "data"
                    newFile.Write(BitConverter.GetBytes(dataWeb.Count), 0, 4); //Количество байт в области данных.

                    newFile.Write(bufferReceive.Array, 0, dataWeb.Count); //Непосредственно WAV-данные.
                    FileData = newFile;
                    newFile.Close();
                    Console.WriteLine("Создаем файл");
                    File.WriteAllBytes(@"C:\WSTTS\Webtts22.wav", FileData.GetBuffer());






                    //}


                    // запись в файл

                    //using (var fstream = new FileStream(@"C:\WSTTS\Webtts.wav", FileMode.OpenOrCreate))
                    //{
                    //    //var array = Encoding.Default.GetBytes(text);
                    //    // асинхронная запись массива байтов в файл
                    //    bufferStream.AddSamples(bufferReceive.Array, 0, dataWeb.Count);
                    //    //File.WriteAllBytes(@"C:\tts.wav", bufferStream);
                    //    ////fstream.WriteByte(bufferStream);
                    //    //Console.WriteLine(bufferStream);
                    //    ////await fstream.WriteAsync(bufferReceive.Array, 0, bufferReceive.Count);
                    //    //Console.WriteLine("Текст записан в файл");

                    //}
                }
            }
        }
    }
}

