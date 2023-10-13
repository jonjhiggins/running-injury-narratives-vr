using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MerryGoRoundAthleteStories : MonoBehaviour
{
    public GameObject athleteStoryObject;
    public AudioClip[] athleteStoryAudioVO;
    public AudioSource audioSource;
    public UnityEvent audioClipEnd;

    private int currentAudioClipIndex = 0;

    // Update is called once per frame
    public void HandleAudioVOClipEnd(int audioClipIndex)
    {
        currentAudioClipIndex = audioClipIndex;
        athleteStoryObject.SetActive(true);

    }

    public void OnSelect()
    {
        StartCoroutine(PlayAudio());
    }

    public void OnUnselect()
    {
        audioSource.Pause();
    }

    IEnumerator PlayAudio()
    {
        Debug.Log(audioSource.time);
        Debug.Log(audioSource.clip != null ? audioSource.clip.length : null);
        if (audioSource.isPlaying || currentAudioClipIndex > athleteStoryAudioVO.Length - 1)
        {
            yield break;
        }
        audioSource.clip = athleteStoryAudioVO[currentAudioClipIndex];
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        // Handle paused audio
        if (audioSource.time < audioSource.clip.length)
        {
            yield break;
        }
        HandleAudioEnd();
    }

    private void HandleAudioEnd()
    {
        audioClipEnd.Invoke();
        athleteStoryObject.SetActive(false);
    }
}
