using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Int32;

namespace CloudTTS
{
    public class Languages
    {
        [JsonPropertyName("id")] 
        public string Id { get; set; }

        [JsonPropertyName("name")] 
        public string Name { get; set; }

        public static async Task<Languages> Request(Task<string> sessionId)
        {
            using (var apiClient = new HttpClient())
            {
                apiClient.DefaultRequestHeaders.Add("X-Session-Id", sessionId.Result);
                Console.WriteLine("Список доступных языков");
                var langsString = await apiClient.GetStringAsync("https://cloud.speechpro.com/vktts/rest/v1/languages");
                var langs = JsonSerializer.Deserialize<Languages[]>(langsString).OrderBy(lang => lang.Id);
                foreach (var lang in langs)
                {
                    Console.WriteLine(lang.Id + " - " + lang.Name);
                }

                Console.WriteLine("\nВыберите язык");
                while (true)
                {
                    var key = Console.ReadLine();
                    if (TryParse(key, out var number) & (number >= 0) & (number <= langs.Count() - 1))
                    {
                        Console.WriteLine(langs.ElementAt(number).Name);
                        return langs.ElementAt(number);
                    }

                    Console.WriteLine("Введен неверный номер");
                }

                //while (true)
                //{
                //    var key = Console.ReadKey().Key;
                //    switch (key)
                //    {
                //        case ConsoleKey.NumPad0:
                //        case ConsoleKey.D0:
                //            Console.WriteLine("\n");
                //            return langs.ElementAt(0);
                //        case ConsoleKey.NumPad1:
                //        case ConsoleKey.D1:
                //            Console.WriteLine("\n");
                //            return langs.ElementAt(1);
                //        case ConsoleKey.NumPad2:
                //        case ConsoleKey.D2:
                //            Console.WriteLine("\n");
                //            return langs.ElementAt(2);
                //        default:
                //            Console.WriteLine("\nВведен неверный номер");
                //            break;
                //    }
                //}
            }
        }

        public StringContent ToJsonContent()
        {
            var json = JsonSerializer.Serialize(this);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}