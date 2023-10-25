using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#nullable enable


public class MerryGoRoundAthleteStories : MonoBehaviour
{
    [SerializeField]
    private AthleteStory[] athleteStories;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private UnityEvent audioClipEnd;

    private AthleteStory? currentAthleteStory = null;

    // Update is called once per frame
    public void HandleAudioVOClipEnd(int audioClipIndex)
    {
        currentAthleteStory = audioClipIndex < athleteStories.Length ? athleteStories[audioClipIndex] : null;
        if (currentAthleteStory == null)
        {
            return;
        }
        currentAthleteStory.merryGoRoundAthleteStories = this;
        currentAthleteStory.athleteStoryObject.SetActive(true);

    }

    public IEnumerator PlayAudio(AudioClip audioClip)
    {
        if (audioSource.isPlaying)
        {
            yield break;
        }
        audioSource.clip = audioClip;
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        HandleAudioEnd();

    }

    private void HandleAudioEnd()
    {
        audioClipEnd.Invoke();
        currentAthleteStory?.athleteStoryObject.SetActive(false);
    }
}
#nullable disable