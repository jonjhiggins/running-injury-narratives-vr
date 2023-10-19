using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleteStoryConstantCycle : AthleteStory
{
    [SerializeField]
    private Animator animator;

    public override void OnSelect()
    {
        PlayAudio();
        animator.enabled = true;
    }

}
