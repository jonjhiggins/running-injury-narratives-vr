using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeCrack : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Material material;
    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private float timeToStart;

    public void Play()
    {
        StartCoroutine(Crack());
    }

    private IEnumerator Crack()
    {
        yield return new WaitForSeconds(timeToStart);
        audioSource.Play();
        _renderer.material = material;
    }
}
