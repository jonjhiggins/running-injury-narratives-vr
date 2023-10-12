using Meta.WitAi.TTS.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MerryGoRoundTTS : MonoBehaviour
{
    [SerializeField] private TTSSpeaker _speaker;
    [TextArea]
    [SerializeField] private string textToSpeak;
    void Start()
    {
        _speaker.Speak(textToSpeak);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
