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
            var credentails = new Credentials(1623, "sorokin-s@speechpro.com", "x9q1yRqB&X");
            var createSession = Session.Create(credentails);
            createSession.Wait();
            //var langReauest = Languages.Request(createSession);
            //langReauest.Wait();
            //var voicesRequest = Voices.Request(createSession, langReauest);
            //voicesRequest.Wait();
            var synthesisWebsocket = SynthesisWebsocket.Synthesize(createSession);
            synthesisWebsocket.Wait();
            //var synthesisPackage = SynthesisPackage.Synthesize(createSession);
            //synthesisPackage.Wait();
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadLine();
        }


        public static async Task Echo()
        {
            using (var ws = new ClientWebSocket())
            {
                var res = "";
                var serverUri =
                    new Uri(
                        "wss://cp.speechpro.com/df55557b-aa41-55cd-907c-5f286769d77c/synthesize/stream/f83c0c5b-9bc4-4af2-a508-bb4b113db00c");
                await ws.ConnectAsync(serverUri, CancellationToken.None);
                while (ws.State == WebSocketState.Open)
                {
                    Console.Write("Input message ('exit' to exit): ");
                    var message = Console.ReadLine();
                    if (message == "exit")
                    {
                    }

                    var bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                    await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
                    var bytesReceived = new ArraySegment<byte>(new byte[1024]);
                    var result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
                    Console.WriteLine("Создаем файл");
                    res = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                }

                // создаем каталог для файла
                var path = @"C:\WSTTS";
                var dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                    Console.WriteLine("Файл создан");
                }

                var text = res;

                // запись в файл
                using (var fstream = new FileStream($"{path}\\tts.raw", FileMode.OpenOrCreate))
                {
                    var array = Encoding.Default.GetBytes(text);
                    // асинхронная запись массива байтов в файл
                    await fstream.WriteAsync(array, 0, array.Length);
                    Console.WriteLine("Текст записан в файл");
                }
            }
        }
    }
}