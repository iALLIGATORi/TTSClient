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
                var createSession = await new Session().Create(credentials);
                //Console.WriteLine(createSession);
                var languageRequest = await new LanguageController().ToRequest(createSession);
                var voicesRequest = await VoiceController.ToRequest(createSession, languageRequest);
                var mode = new Selection().SelectMode();
                var method = new Selection().SelectMethodInput();
                var text = new TextReader().Reading(method, mode);
                await new Synthesis().Synthesizing(createSession, voicesRequest, mode, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await new Disconnection().Disconnect();
            }

            Console.WriteLine("\nНажмите любую клавишу для завершения");
            Console.ReadLine();
        }
    }
}