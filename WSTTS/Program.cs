using System;
using WSTTS;

namespace CloudTTS
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var credentials = new Credentials(1623, "sorokin-s@speechpro.com", "x9q1yRqB&X");
            var createSession = Session.Create(credentials);
            createSession.Wait();

            //Проверка статуса сессии
            //var statusSession = Session.Status(createSession);
            //statusSession.Wait();

            var languageRequest = Languages.Request(createSession);
            languageRequest.Wait();
            var voicesRequest = Voices.Request(createSession, languageRequest);
            voicesRequest.Wait();

            Console.WriteLine("Выберите режим синтеза:\n1 - Пакетный режим;\n2 - Потоковый режим.");
            while (true)
            {
                var consoleKey = Console.ReadLine();
                int.TryParse(consoleKey, out var key);
                if (key == 1)
                {
                    var synthesisPackage = SynthesisPackage.Synthesize(createSession, voicesRequest);
                    synthesisPackage.Wait();
                    break;
                }

                if (key == 2)
                {
                    var synthesisWebsocket = SynthesisWebsocket.Synthesize(createSession, voicesRequest);
                    synthesisWebsocket.Wait();
                    break;
                }

                Console.WriteLine("Введен неверный номер. Повторите попытку");
            }

            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadLine();
        }
    }
}