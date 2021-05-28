﻿using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudTTS
{
    internal class Credentials
    {
        public Credentials(int domainId, string username, string password)
        {
            DomainId = domainId;
            UserName = username;
            Password = password;
        }

        [JsonPropertyName("domain_id")] public int DomainId { get; set; }

        [JsonPropertyName("username")] public string UserName { get; set; }

        [JsonPropertyName("password")] public string Password { get; set; }

    }
}