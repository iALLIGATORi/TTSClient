using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudTTS
{
    public class Voices
    {
        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("gender")] public string Gender { get; set; }

        public static async Task<Voices> Request(Task<string> sessionId, Task<Languages> language)
        {
            using (var apiClient = new HttpClient())
            {
                apiClient.DefaultRequestHeaders.Add("X-Session-Id", sessionId.Result);
                Console.WriteLine("Список доступных голосов для синтеза");
                var voicesString = await apiClient.GetStringAsync(
                    "https://cloud.speechpro.com/vktts/rest/v1/languages/" + language.Result.Name + "/voices");
                var voices = JsonSerializer.Deserialize<Voices[]>(voicesString).OrderBy(voice => voice.Name);
                int voiceNumber = 0;
                foreach (var voice in voices)
                {
                    Console.WriteLine(voiceNumber + " - " + voice.Name);
                    voiceNumber++;
                }


                Console.WriteLine("\nВыберите голос");
                while (true)
                {
                    var key = Console.ReadLine();
                    if (Int32.TryParse(key, out var number) & (number >= 0) & (number <= voices.Count() - 1))
                    {
                        Console.WriteLine(voices.ElementAt(number).Name);
                        return voices.ElementAt(number);
                    }

                    Console.WriteLine("Введен неверный номер");
                }
            }
        }
    }
}