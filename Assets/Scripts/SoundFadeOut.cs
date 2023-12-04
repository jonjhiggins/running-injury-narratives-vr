using UnityEngine;

public class SoundFadeOut : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeOutTime = 2f;
    public float fadeOutVolume = 0f;

    private bool fadingOut = false;

    private void Start()
    {
        // Make sure to assign an AudioSource component to the variable in the Inspector
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found. Please assign an AudioSource component to the script.");
        }
    }

    private void Update()
    {
        if (fadingOut && audioSource != null && audioSource.volume > fadeOutVolume)
        {
            // Calculate the new volume based on the elapsed time
            float newVolume = Mathf.MoveTowards(audioSource.volume, fadeOutVolume, Time.deltaTime / fadeOutTime);

            // Set the new volume to the AudioSource
            audioSource.volume = newVolume;
        }

        if (fadingOut && audioSource.volume <= fadeOutVolume)
        {
            fadingOut = false;
        }
    }

    public void FadeOut(float newFadeOutVolume = 0)
    {
        fadeOutVolume = newFadeOutVolume;
        fadingOut = true;
    }

}