using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Cloud
{
    internal class Disconnection
    {
        internal async Task Disconnect()
        {
            if ( WebSocketController.WebClient.State == WebSocketState.Open)
            {
                await  WebSocketController.Disconnect();
            }

            await new HttpController().Delete();
        }
    }
}