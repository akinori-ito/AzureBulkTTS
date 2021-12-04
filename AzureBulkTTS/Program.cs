// Bulk Text-to-speech app using Azure Microsoft CognitiveService
using System.IO;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace AzureBulkTTS
{
    public class Config
    {
        public Dictionary<string,string> ConfigTable;
        public Config(string configfile)
        {
            using (var f = new StreamReader(configfile))
            {
                var configstr = f.ReadToEnd();
                ConfigTable = JsonSerializer.Deserialize<Dictionary<string,string>>(configstr);
                string[] requiredKeys = { "SubscriptionKey", "Region", "Language", "Voice" };
                foreach (string key in requiredKeys) {
                    if (!ConfigTable.ContainsKey(key))
                    {
                        throw new Exception("Config: key not specified: " + key);
                    }
                }
            }
        }
        public SpeechConfig getSpeechConfig()
        {
            var conf = SpeechConfig.FromSubscription(
                ConfigTable["SubscriptionKey"], ConfigTable["Region"]
                );
            conf.SpeechSynthesisLanguage = ConfigTable["Language"];
            conf.SpeechSynthesisVoiceName = ConfigTable["Voice"];
            return conf;
        }
    }
    class Program
    {
        static void SynthesizeToWav(Config config, string text, string wavfile)
        {
            using var audio = AudioConfig.FromWavFileOutput(wavfile);
            using var synth = new SpeechSynthesizer(config.getSpeechConfig(), audio);
            synth.SpeakTextAsync(text).Wait();
        }
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                throw new Exception("Usage: AzureBulkTTS configfile textlist");
            }
            var config = new Config(args[0]);
            var f = new StreamReader(args[1]);
            int k = 1;
            while (true)
            {
                var line = f.ReadLine();
                if (line == null)
                    break;
                SynthesizeToWav(config, line, String.Format("{0:000}.wav", k));
                k++;
            }
        }
    }
}
