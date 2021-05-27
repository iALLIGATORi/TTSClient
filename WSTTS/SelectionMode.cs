using System;
using System.IO;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class SelectionMode
    {
        internal static string Input()
        {
            Console.WriteLine("Введите текст для синтеза");
            Console.SetIn(new StreamReader(Console.OpenStandardInput(81920), Console.InputEncoding, false, 81920));
            string inputText;
            while (true)
            {
                inputText = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputText))
                {
                    break;
                }

                Console.WriteLine("Необходимо ввести текст для синтеза");
            }

            return inputText;
        }

        public static async Task Select(string session, Voices voice)
        {
            Console.WriteLine("Выберите режим синтеза:\n1 - Пакетный режим;\n2 - Потоковый режим.");
            while (true)
            {
                var consoleKey = Console.ReadLine();
                int.TryParse(consoleKey, out var key);
                if (key == 1)
                {
                    string text;
                    while (true)
                    {
                        text = Input();
                        if (text.Length < 500)
                        {
                            break;
                        }

                        Console.WriteLine("\nВведен текст длиной " + text.Length +
                                          " символов. Этот режим имеет ограничение по длине обрабатываемого текста(до 500 символов).\n");
                    }

                    await Synthesis.Packadge(session, voice, text);

                    break;
                }

                if (key == 2)
                {
                    var text = Input();
                    await Synthesis.Websocket(session, voice, text);

                    break;
                }

                Console.WriteLine("Введен неверный номер. Повторите попытку");
            }
        }
    }
}