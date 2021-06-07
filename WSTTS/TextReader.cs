using System;
using System.IO;
using System.Threading.Tasks;

namespace Cloud
{
    internal class TextReader
    {
        internal string Reading(int keyMethod, int keyMode)
        {
            if (keyMethod == 1)
            {
                var consoleText = ReadingIsConsole(keyMode);
                return consoleText;
            }

            var fileText = ReadingIsFile(keyMode).Result;
            return fileText;
        }

        private string ReadingIsConsole(int keyMode)
        {
            Console.WriteLine("\nВведите текст для синтеза");
            var bufferSize = 502;
            if (keyMode == 2)
            {
                bufferSize = 10002;
            }

            Console.SetIn(new StreamReader(Console.OpenStandardInput(bufferSize), Console.InputEncoding, false,
                bufferSize));

            while (true)
            {
                var text = Console.ReadLine();

                if (!string.IsNullOrEmpty(text))
                {
                    return text;
                }

                Console.WriteLine("Необходимо ввести текст для синтеза. Попробуйте еще раз.");
            }
        }

        private async Task<string> ReadingIsFile(int keyMode)
        {
            Console.WriteLine("\nВведите полный путь до файла");
            FileInfo file;
            while (true)
            {
                var pathFile = Console.ReadLine();
                file = new FileInfo(pathFile);
                if (file.Extension == ".txt")
                {
                    break;
                }

                Console.WriteLine("Неверный формат файла");
            }

            var limitChar = 500;
            if (keyMode == 2)
            {
                limitChar = 10000;
            }

            var buffer = new char[135];
            string text = null;
            var limit = 0;
            using var sr = file.OpenText();
            while (true)
            {
                var result = await sr.ReadBlockAsync(buffer, 0, buffer.Length);
                limit += result;
                if (result == 0)
                {
                    break;
                }

                if (limit == limitChar)
                {
                    text += new string(buffer, 0, result);
                    break;
                }

                if (limit > limitChar)
                {
                    var resultLimit = limitChar - text.Length;
                    text += new string(buffer, 0, resultLimit);
                    break;
                }

                text += new string(buffer, 0, result);
            }

            //Console.WriteLine(text.Length);
            return text;
        }
    }
}