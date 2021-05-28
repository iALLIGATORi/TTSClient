﻿using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudTTS
{
    internal class LanguagesRequest
    {
        public static async Task<Languages> Request(string sessionId)
        {
            var requestUri = "https://cp.speechpro.com/vktts/rest/v1/languages";
            HttpClientFactory.Create(sessionId);
            var request = await HttpRequest.Get(requestUri);
            var languages = JsonSerializer.Deserialize<Languages[]>(request).OrderBy(lang => lang.Id);
            return Selection.SelectLanguage(languages);
        }

    }
}