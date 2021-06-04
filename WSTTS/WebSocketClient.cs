using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class WebSocketClient
    {
        internal static readonly ClientWebSocket WebClient = new ClientWebSocket();

        internal static async Task<ClientWebSocket> Connect(Uri uri)
        {
            await WebClient.ConnectAsync(uri, CancellationToken.None);
            Console.WriteLine(uri);
            return WebClient;
        }
    }
}