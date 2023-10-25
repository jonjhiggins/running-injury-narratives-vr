using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeCrack : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float timeToStart;
    [SerializeField]
    private MeshDestroy meshDestroy;
    private bool hasCracked;

    public void Play()
    {
        if (hasCracked)
        {
            return;
        }
        StartCoroutine(Crack());
        hasCracked = true;
    }

    private IEnumerator Crack()
    {
        yield return new WaitForSeconds(timeToStart);
        audioSource.Play();
        meshDestroy.DestroyMesh();
    }
}
