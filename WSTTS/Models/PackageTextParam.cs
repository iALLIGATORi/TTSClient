﻿using System.Text.Json.Serialization;

namespace Cloud
{
    public class PackageTextParam
    {
        public PackageTextParam(string value)
        {
            Mime = "text/plain";
            Value = value;
        }

        [JsonPropertyName("mime")] public string Mime { get; set; }

        [JsonPropertyName("value")] public string Value { get; set; }
    }
}