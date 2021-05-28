using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTTS
{
    class ConsoleInput
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
    }
}
