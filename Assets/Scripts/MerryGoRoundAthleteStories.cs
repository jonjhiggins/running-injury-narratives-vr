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
    private bool isPlaying;

    // Update is called once per frame
    public void HandleAudioVOClipEnd(int audioClipIndex)
    {
        currentAudioClipIndex = audioClipIndex;
        athleteStoryObject.SetActive(true);

    }

    public void GrabEvent()
    {
        Debug.Log("grab");
        StartCoroutine(PlayAudio());
    }

    IEnumerator PlayAudio()
    {
        if (isPlaying || currentAudioClipIndex > athleteStoryAudioVO.Length - 1)
        {
            yield break;
        }
        audioSource.clip = athleteStoryAudioVO[currentAudioClipIndex];
        audioSource.Play();
        isPlaying = true;
        yield return new WaitWhile(() => audioSource.isPlaying);
        HandleAudioEnd();
    }

    private void HandleAudioEnd()
    {
        isPlaying = false;
        audioClipEnd.Invoke();
        athleteStoryObject.SetActive(false);
    }
}
