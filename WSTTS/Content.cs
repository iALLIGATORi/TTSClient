﻿using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CloudTTS
{
    internal class Content
    {
        public static StringContent ToJson(object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }
    }
}