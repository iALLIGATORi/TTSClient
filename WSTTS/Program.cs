using System;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var credentials = new Credentials(1623, "sorokin-s@speechpro.com", "x9q1yRqB&X");
                var createSession = await Session.Create(credentials);
                var languageRequest = await LanguagesRequest.Request(createSession);
                Console.WriteLine(createSession);
                var voicesRequest = await VoicesRequest.Request(createSession, languageRequest);
                var keyMode = Selection.SelectMode();
                var keyMethod = Selection.SelectMethodInput();
                var text = TextReader.Reading(keyMethod, keyMode);
                await Synthesis.Synthesizing(createSession, voicesRequest, keyMode, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.WriteLine("\nНажмите любую клавишу для завершения");
            Console.ReadLine();
        }
    }
}