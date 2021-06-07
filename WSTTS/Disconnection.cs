using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Cloud
{
    internal class Disconnection
    {
        internal async Task Disconnect()
        {
            if (new WebSocketController().WebClient.State == WebSocketState.Open)
            {
                await new WebSocketController().Disconnect();
            }

            await new HttpController().Delete();
        }
    }
}