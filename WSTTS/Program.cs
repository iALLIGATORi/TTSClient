using System;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var credentials = new Credentials(1623, "sorokin-s@speechpro.com", "x9q1yRqB&X");
            var createSession = await Session.Create(credentials);

            //Проверка статуса сессии
            //var statusSession = Session.Status(createSession);
            //statusSession.Wait();

            var languageRequest = await LanguagesRequest.Request(createSession);
            var voicesRequest = await VoicesRequest.Request(createSession, languageRequest);
            await SelectionMode.Select(createSession, voicesRequest);

            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadLine();
        }
    }
}