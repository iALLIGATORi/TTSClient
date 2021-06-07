using System;
using System.Text.Json;

namespace Cloud
{
    internal class SoundConverter
    {
        internal static byte[] Base64ToSound(string base64Json)
        {
            var base64 = JsonSerializer.Deserialize<Sound>(base64Json);
            if (base64 == null)
            {
                Console.WriteLine("Нет звуковых данных");
            }

            var sound = Convert.FromBase64String(base64.Data);
            return sound;
        }
    }
}