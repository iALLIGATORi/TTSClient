using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Cloud
{
    internal class WebSocketController
    {
        internal static readonly ClientWebSocket WebClient = new ClientWebSocket();

        internal static async Task Connect(Uri uri)
        {
            await WebClient.ConnectAsync(uri, CancellationToken.None);
        }

        internal static async Task Disconnect()
        {
            await WebClient.CloseAsync(WebSocketCloseStatus.NormalClosure, null!, CancellationToken.None);
        }

        internal static async Task Send(byte[] messageByte)
        {
            var bufferSend = new ArraySegment<byte>(messageByte);
            await WebClient.SendAsync(bufferSend, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        internal static async Task Receive(int sampleRate)
        {
            var bufferReceive = new ArraySegment<byte>(new byte[8192]);

            using (var memoryStream = new MemoryStream())
            {
                while (true)
                {
                    var result = await WebClient.ReceiveAsync(bufferReceive, CancellationToken.None);
                    await memoryStream.WriteAsync(bufferReceive.Array, 0, result.Count, CancellationToken.None);
                    if ((result.Count == 0) & result.EndOfMessage)
                    {
                        break;
                    }
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                FileSaver.Save(memoryStream, sampleRate);
            }
        }
    }
}