using System;
using System.Linq;

namespace Cloud
{
    internal class Selection
    {
        public Languages SelectLanguage(IOrderedEnumerable<Languages> languages)
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
                    return languages.ElementAt(number);
                }

                Console.WriteLine("Введен неверный номер");
            }
        }

        public Voices SelectVoice(IOrderedEnumerable<Voices> voices)
        {
            Console.WriteLine("\nСписок доступных голосов для синтеза");
            var voiceNumber = 1;
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
                    return voices.ElementAt(number);
                }

                Console.WriteLine("Введен неверный номер");
            }
        }

        public int SelectMode()
        {
            Console.WriteLine(
                "\nДоступные режимы синтеза:\n1 - Пакетный режим;\n2 - Потоковый режим.\n\nВыберите режим:");
            while (true)
            {
                var consoleKey = Console.ReadLine();
                int.TryParse(consoleKey, out var keyMode);
                if (keyMode == 1)
                {
                    return keyMode;
                }

                if (keyMode == 2)
                {
                    return keyMode;
                }

                Console.WriteLine("Введен неверный номер. Повторите попытку");
            }
        }

        public int SelectMethodInput()
        {
            Console.WriteLine("\nДоступные способы ввода текста:\n1 - Консоль;\n2 - Файл.\n\nВыберите способ:");
            while (true)
            {
                var consoleKey = Console.ReadLine();
                int.TryParse(consoleKey, out var keyMethod);
                if (keyMethod == 1)
                {
                    return keyMethod;
                }

                if (keyMethod == 2)
                {
                    return keyMethod;
                }

                Console.WriteLine("Введен неверный номер. Повторите попытку");
            }
        }
    }
}