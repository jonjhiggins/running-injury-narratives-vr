using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MerryGoRoundVO : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _VOFiles;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private UnityEvent<int> audioClipEnd;
    [SerializeField]
    private int currentAudioClipIndex = 0;
    [SerializeField]
    private bool skipToAthleteStory;

    void Start()
    {
        if (skipToAthleteStory)
        {
            audioClipEnd.Invoke(currentAudioClipIndex);
            return;
        }
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
