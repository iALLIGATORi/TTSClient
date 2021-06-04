namespace CloudTTS
{
    internal class SampleRate
    {
        internal static int SamplingRate(Voices voice)
        {
            var rate = "8000";
            var sampleRate = 22000;
            if (voice.Name.Contains(rate))
            {
                sampleRate = 8000;
            }

            return sampleRate;
        }
    }
}