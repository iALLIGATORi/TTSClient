using System;
using System.Text.Json;

namespace Cloud
{
    internal class UrlConverter
    {
        internal Uri ConvertingToUri(string urlJson)
        {
            var url = JsonSerializer.Deserialize<WebsocketUrl>(urlJson);
            if (url == null)
            {
                throw new ArgumentNullException("Нет звуковых данных");
            }

            var uri = new Uri(url.Url);
            return uri;
        }
    }
}