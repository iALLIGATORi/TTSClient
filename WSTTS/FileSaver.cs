﻿using System;
using System.IO;
using NAudio.Wave;

namespace CloudTTS
{
    internal class FileSaver
    {
        private static readonly string Path = @"C:\Cloud\";

        private static DirectoryInfo DirectoryCreate()
        {
            Console.WriteLine("Создаем файл");
            var dirPath = new DirectoryInfo(Path);
            if (!dirPath.Exists)
            {
                dirPath.Create();
            }

            return dirPath;
        }

        public static void Save(byte[] sound)
        {
            var dir = DirectoryCreate();
            var wavName = "packadge.wav";
            File.WriteAllBytes(Path + wavName, sound);
            Console.WriteLine("Файл " + wavName + " размером " + sound.Length +
                              " байт сохранен в папке " + dir);
        }

        public static void Save(MemoryStream ms, int sampleRate)
        {
            var dir = DirectoryCreate();
            var wavName = "websocket.wav";
            var pcmToWave = new RawSourceWaveStream(ms, new WaveFormat(sampleRate, 1));
            WaveFileWriter.CreateWaveFile(Path + wavName, pcmToWave);
            Console.WriteLine("Файл " + wavName + " размером " + ms.Length +
                              " байт сохранен в папке " + dir);
        }
    }
}