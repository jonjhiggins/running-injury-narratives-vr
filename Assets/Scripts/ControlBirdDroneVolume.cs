using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBirdDroneVolume : MonoBehaviour
{
    private AudioSource birdsDrone;

    public void SetVolume(float volume)
    {
        GameObject birdsDroneGameObject = GameObject.FindGameObjectWithTag("birds-drone");
        if (birdsDroneGameObject != null)
        {
            birdsDroneGameObject.GetComponent<AudioSource>().volume = volume;
        }
    }
}
