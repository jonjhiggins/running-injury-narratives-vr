using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#nullable enable


public class MerryGoRoundAthleteStories : MonoBehaviour
{
    [Serializable]
    private struct AthleteStory
    {
        public GameObject athleteStoryObject;
        public AudioClip athleteStoryAudioVO;
    }

    [SerializeField]
    private AthleteStory[] athleteStories;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private UnityEvent audioClipEnd;
    [SerializeField]
    private GameObject defaultAthleteStoryObject;
    [SerializeField]
    private bool useDefaultAthleteStoryObject;

    private AthleteStory? currentAthleteStory = null;

    // Update is called once per frame
    public void HandleAudioVOClipEnd(int audioClipIndex)
    {
        currentAthleteStory = audioClipIndex < athleteStories.Length ? athleteStories[audioClipIndex] : null;

        var athleteGameObject = GetCurrentAthleteStoryObject();
        athleteGameObject?.SetActive(true);

    }

    // For user testing we want to be able to have a single basic object for all athlete stories
    // Can be removed following user test
    
    private GameObject? GetCurrentAthleteStoryObject()
    
    {
        return useDefaultAthleteStoryObject ? defaultAthleteStoryObject : currentAthleteStory?.athleteStoryObject;
    }

    public void OnSelect()
    {
        StartCoroutine(PlayAudio());
    }

    IEnumerator PlayAudio()
    {
        if (audioSource.isPlaying || currentAthleteStory == null)
        {
            yield break;
        }
        audioSource.clip = currentAthleteStory?.athleteStoryAudioVO;
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        // Handle paused audio
        if (audioSource.time < audioSource?.clip?.length)
        {
            yield break;
        }
        HandleAudioEnd();
    }

    private void HandleAudioEnd()
    {
        audioClipEnd.Invoke();
        var athleteGameObject = GetCurrentAthleteStoryObject();
        athleteGameObject?.SetActive(false);
    }
}
#nullable disable