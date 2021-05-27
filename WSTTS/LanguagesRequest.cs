using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class LanguagesRequest
    {
        public static async Task<Languages> Request(string sessionId)
        {
            var requestUri = "https://cp.speechpro.com/vktts/rest/v1/languages";
            HttpClientFactory.Create(sessionId);

            Console.WriteLine("Список доступных языков");

            var langsString = await HttpClientFactory.Get(requestUri);
            var langs = JsonSerializer.Deserialize<Languages[]>(langsString).OrderBy(lang => lang.Id);
            foreach (var lang in langs)
            {
                Console.WriteLine(lang.Id + " - " + lang.Name);
            }

            Console.WriteLine("\nВыберите язык");
            while (true)
            {
                var key = Console.ReadLine();
                if (int.TryParse(key, out var number) & (number >= 0) & (number <= langs.Count() - 1))
                {
                    Console.WriteLine(langs.ElementAt(number).Name);
                    return langs.ElementAt(number);
                }

                Console.WriteLine("Введен неверный номер");
            }
        }

    }
}