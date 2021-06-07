using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Cloud
{
    internal class Disconnection
    {
        internal static async Task Disconnect()
        {
            if (WebSocketController.WebClient.State == WebSocketState.Open)
            {
                await WebSocketController.Disconnect();
            }
            
            await HttpController.Delete();
        }
    }
}