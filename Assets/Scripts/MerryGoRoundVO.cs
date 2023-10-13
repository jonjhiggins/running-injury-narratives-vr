using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MerryGoRoundVO : MonoBehaviour
{
    public AudioClip[] _VOFiles;
    public AudioSource audioSource;
    public UnityEvent<int> audioClipEnd;

    private int currentAudioClipIndex = 0;

    void Start()
    {
        StartCoroutine(LoadAndPlayAudio());
    }

    IEnumerator LoadAndPlayAudio()
    {
        if (currentAudioClipIndex > _VOFiles.Length - 1) {
            yield break;
        }
        audioSource.clip = _VOFiles[currentAudioClipIndex];
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        audioClipEnd.Invoke(currentAudioClipIndex);
    }

    public void HandleAudioAthleteVOClipEnd()
    {
        currentAudioClipIndex += 1;
        StartCoroutine(LoadAndPlayAudio());
    }
}
