using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class SynthesisPackage
    {
        public static async Task Synthesize(Task<string> sessionId, Task<Voices> voiceRequest)
        {
            using (var apiClient = new HttpClient())
            {
                apiClient.DefaultRequestHeaders.Add("X-Session-Id", sessionId.Result);
                Console.WriteLine("Введите текст для синтеза");
                var value = Console.ReadLine();
                var synthesizeText = new SynthesizeText(value);
                try
                {
                    var synthesizeContent = new SynthesizeRequest(voiceRequest.Result.Name, synthesizeText).ToJsonContent();

                    var synthesizeResponse = await apiClient.PostAsync("https://cp.speechpro.com/vktts/rest/v1/synthesize",
                        synthesizeContent);
                    var base64String = await synthesizeResponse.Content.ReadAsStringAsync();
                    var base64 = JsonSerializer.Deserialize<Sound>(base64String);
                    if (base64 == null)
                    {
                        Console.WriteLine("Нет звуковых данных");
                    }

                    var sound = Convert.FromBase64String(base64.Data);
                    File.WriteAllBytes(@"C:\WSTTS\tts.wav", sound);

                    Console.WriteLine("Файл tts.wav размером " + sound.Length +
                                      " байт записан в корневую папку на диск С");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}