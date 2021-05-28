using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class Selection
    {
        public static Languages SelectLanguage(IOrderedEnumerable<Languages> languages)
        {
            Console.WriteLine("Список доступных языков");

            foreach (var lang in languages)
            {
                Console.WriteLine(lang.Id + " - " + lang.Name);
            }

            Console.WriteLine("\nВыберите язык");
            while (true)
            {
                var key = Console.ReadLine();
                if (int.TryParse(key, out var number) & (number >= 0) & (number <= languages.Count() - 1))
                {
                    //Console.WriteLine(languages.ElementAt(number).Name);
                    return languages.ElementAt(number);
                }

                Console.WriteLine("Введен неверный номер");
            }
        }

        public static Voices SelectVoice(IOrderedEnumerable<Voices> voices)
        {
            Console.WriteLine("\nСписок доступных голосов для синтеза");
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
                    //Console.WriteLine(voices.ElementAt(number).Name);
                    return voices.ElementAt(number);
                }

                Console.WriteLine("Введен неверный номер");
            }
        }
        public static async Task SelectMode(string session, Voices voice)
        {
            Console.WriteLine("\nВыберите режим синтеза:\n1 - Пакетный режим;\n2 - Потоковый режим.");
            while (true)
            {
                var consoleKey = Console.ReadLine();
                int.TryParse(consoleKey, out var key);
                if (key == 1)
                {
                    string text;
                    while (true)
                    {
                        text = ConsoleInput.Input();
                        if (text.Length < 500)
                        {
                            break;
                        }

                        Console.WriteLine("\nВведен текст длиной " + text.Length +
                                          " символов. Этот режим имеет ограничение по длине обрабатываемого текста(до 500 символов).\n");
                    }

                    await Synthesis.Package(session, voice, text);

                    break;
                }

                if (key == 2)
                {
                    var text = ConsoleInput.Input();
                    await Synthesis.Websocket(session, voice, text);

                    break;
                }

                Console.WriteLine("Введен неверный номер. Повторите попытку");
            }
        }
    }
}