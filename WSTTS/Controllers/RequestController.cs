namespace Cloud
{
    internal class RequestController
    {
        internal static object RequestWebsocket(Voices voice)
        {
            var webParam = new WebSocketTextParam();
            return new DataWebSocket(voice.Name, webParam);
        }

        internal static object RequestPackage(Voices voice, string text)
        {
            var packageParam = new PackageTextParam(text);
            return new DataPackage(voice.Name, packageParam);
        }
    }
}