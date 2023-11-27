using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseIfNotCloseToHeadset : MonoBehaviour
{
    [SerializeField]
    private CheckIfCloseToHeadset checkIfCloseToHeadset;
    [SerializeField]
    private PlayableDirector timeline;

    public void Pause()
    {
        // Don't pause timeline if object is already close to headset
        if (checkIfCloseToHeadset.IsCloseToHeadset())
        {
            return;
        }
        timeline.Pause();
    }
}
