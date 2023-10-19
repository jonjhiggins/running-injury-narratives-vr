using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleteStoryConstantCycle : AthleteStory
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float startRotateTime;
    [SerializeField]
    private float stopRotateTime;

    IEnumerator StartRotation()
    {
        yield return new WaitForSeconds(startRotateTime);
        animator.SetBool("Rotate", true);
        StartCoroutine(StopRotation());
    }

    IEnumerator StopRotation()
    {
        float rotationDuration = stopRotateTime - startRotateTime;
        yield return new WaitForSeconds(rotationDuration);
        animator.SetBool("Rotate", false);
    }

    public override void OnSelect()
    {
        PlayAudio();
        StartCoroutine(StartRotation());
    }

}
