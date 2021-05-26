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
                    var synthesizeContent =
                        new PackageRequest(voiceRequest.Result.Name, synthesizeText).ToJsonContent();

                    var synthesizeResponse = await apiClient.PostAsync(
                        "https://cp.speechpro.com/vktts/rest/v1/synthesize",
                        synthesizeContent);
                    var base64String = await synthesizeResponse.Content.ReadAsStringAsync();
                    var base64 = JsonSerializer.Deserialize<Sound>(base64String);
                    if (base64 == null)
                    {
                        Console.WriteLine("Нет звуковых данных");
                    }

                    var sound = Convert.FromBase64String(base64.Data);


                    var dir = new DirectoryInfo(@"C:\Cloud\");
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    var wavName = "packadge.wav";
                    var pathFile = dir + wavName;
                    File.WriteAllBytes(pathFile, sound);
                    Console.WriteLine("Файл " + wavName + " размером " + sound.Length +
                                      " байт сохранен в папке " + dir);

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