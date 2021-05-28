using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudTTS
{
    class HttpRequest
    {
        public static Task<HttpResponseMessage> Post(string requestUri, HttpContent content)
        {
            var client = HttpClientFactory.Client;
            return client.PostAsync(requestUri, content);
        }

        public static Task<string> Get(string requestUri)
        {
            var client = HttpClientFactory.Client;
            return client.GetStringAsync(requestUri);
        }
    }
}
