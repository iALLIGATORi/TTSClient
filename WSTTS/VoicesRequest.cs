using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class VoicesRequest
    {
        public static async Task<Voices> Request(string sessionId, Languages language)
        {
            var requestUri = "https://cloud.speechpro.com/vktts/rest/v1/languages/";

            Console.WriteLine("Список доступных голосов для синтеза");
            var voicesString = await HttpClientFactory.Get(requestUri + language.Name + "/voices");
            var voices = JsonSerializer.Deserialize<Voices[]>(voicesString).OrderBy(voice => voice.Name);
            var voiceNumber = 0;
            foreach (var voice in voices)
            {
                Console.WriteLine(voiceNumber + " - " + voice.Name);
                voiceNumber++;
            }


            Console.WriteLine("\nВыберите голос");
            while (true)
            {
                var key = Console.ReadLine();
                if (int.TryParse(key, out var number) & (number >= 0) & (number <= voices.Count() - 1))
                {
                    Console.WriteLine(voices.ElementAt(number).Name);
                    return voices.ElementAt(number);
                }

                Console.WriteLine("Введен неверный номер");
            }
        }
    }
}