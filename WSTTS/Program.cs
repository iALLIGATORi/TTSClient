using System;
using System.Threading.Tasks;

namespace Cloud
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var credentials = new Credentials(1623, "sorokin-s@speechpro.com", "x9q1yRqB&X");
                var createSession = await Session.Create(credentials);
                var languageRequest = await LanguageController.ToRequest(createSession);
                var voicesRequest = await VoiceController.ToRequest(createSession, languageRequest);
                var mode = Selection.SelectMode();
                var method = Selection.SelectMethodInput();
                var text = TextReader.Reading(method, mode);
                await Synthesis.Synthesizing(createSession, voicesRequest, mode, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await Disconnection.Disconnect();
            }

            Console.WriteLine("\nНажмите любую клавишу для завершения");
            Console.ReadLine();
        }
    }
}