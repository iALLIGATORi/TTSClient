using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudTTS
{
    class SoundConverter
    {
        internal static void Base64ToSound(string base64Json)
        {
            var base64 = JsonSerializer.Deserialize<Sound>(base64Json);
            if (base64 == null)
            {
                Console.WriteLine("Нет звуковых данных");
            }

            var sound = Convert.FromBase64String(base64.Data);

            FileSaver.Save(sound);
        }

    }
}
