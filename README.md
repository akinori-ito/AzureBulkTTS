# AzureBulkTTS
Generate wav files from text using Azure CognitiveService.Speech.

## Installation
Compile source with VisualStudio/C#. It needs Microsoft.CognitiveService.Speech library, which can be obtained from nuget.

## Azure CognitiveService Subscription
This program uses Azure Text-to-Speech service. Thus, the user need to subscribe Asure Speech service.
After subscription, please make sure you have the subscription key and region information.

## Config file
Prepare the config file in JSON format, such as "config.json," that looks like the following:

```{JSON}
{
    "SubscriptionKey" : "Your-Subscription-Key",
    "Region" : "Your region",
    "Language" : "Your language (such as ja-JP)",
    "Voice" : "Name of voice"
}
```
The list of voice name can be found [here](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/rest-text-to-speech).

## Usage
```
AzureBulkTTS configfile sentencelistfile
```

- `configfile` : The configuration file explained above
- `sentencelistfile` : The text file of the sentences, one line for one sentence

The output files are numbered based on the line number of the sentence, such as `001.wav`.
