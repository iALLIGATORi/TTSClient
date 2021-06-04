using System;
using System.Text.Json;

namespace CloudTTS
{
    internal class UrlConverter
    {
        internal static Uri ConvertingToUri(string urlJson)
        {
            var url = JsonSerializer.Deserialize<WebsocketUrl>(urlJson);
            if (url == null)
            {
                Console.WriteLine("Нет звуковых данных");
            }

            var uri = new Uri(url.Url);
            return uri;
        }
    }
}