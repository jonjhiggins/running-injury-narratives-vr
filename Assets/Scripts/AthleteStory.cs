using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AthleteStory : MonoBehaviour
{
    public GameObject athleteStoryObject;
    [SerializeField]
    protected AudioClip athleteStoryAudioVO;
    public MerryGoRoundAthleteStories merryGoRoundAthleteStories;

    // Update is called once per frame
    public void PlayAudio()
    {
        StartCoroutine(merryGoRoundAthleteStories.PlayAudio(athleteStoryAudioVO));
    }

    public abstract void OnSelect();
}
