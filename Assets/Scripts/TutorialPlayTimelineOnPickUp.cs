using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialPlayTimelineOnPickUp : MonoBehaviour {

    [SerializeField]
    private PlayableDirector Timeline;

    // Timeline play is only allowed after a certain frame
    // After that frame, they must not have already picked it up
    private bool allowTimelinePlay;

    public void WhenSelectTutorialObject()
    {
        Debug.Log($"WhenSelectTutorialObject {allowTimelinePlay}");
        if (!allowTimelinePlay)
        {
            return;
        }
        Timeline.Play();
        allowTimelinePlay = false;
    }

    public void AlowTimelinePlay()
    {
        allowTimelinePlay = true;
        Debug.Log($"AlowTimelinePlay {allowTimelinePlay}");
    }

}
