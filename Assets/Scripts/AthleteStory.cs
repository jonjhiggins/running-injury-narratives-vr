using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AthleteStory : MonoBehaviour
{
    public GameObject athleteStoryObject;
    [SerializeField]
    protected AudioClip athleteStoryAudioVO;
    public MerryGoRoundAthleteStories merryGoRoundAthleteStories;
    private bool hasPlayed;

    // Update is called once per frame
    public void PlayAudio()
    {
        if (hasPlayed)
        {
            return;
        }
        StartCoroutine(merryGoRoundAthleteStories.PlayAudio(athleteStoryAudioVO));
        hasPlayed = true;
    }

    public abstract void OnSelect();
}
