using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            var statusSession = Session.Status(createSession);
            statusSession.Wait();
            var languageReauest = Languages.Request(createSession);
            languageReauest.Wait();
            var voicesRequest = Voices.Request(createSession, languageReauest);
            voicesRequest.Wait();
            //var synthesisWebsocket = SynthesisWebsocket.Synthesize(createSession, voicesRequest);
            //synthesisWebsocket.Wait();
            //Console.WriteLine(synthesisWebsocket.Result);
            var synthesisPackage = SynthesisPackage.Synthesize(createSession, voicesRequest);
            synthesisPackage.Wait();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadLine();
        }
    }
}