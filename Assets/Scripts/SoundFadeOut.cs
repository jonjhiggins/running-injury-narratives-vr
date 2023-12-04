using UnityEngine;

public class SoundFadeOut : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeOutTime = 2f;

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
        if (fadingOut && audioSource != null && audioSource.volume > 0)
        {
            // Calculate the new volume based on the elapsed time
            float newVolume = Mathf.MoveTowards(audioSource.volume, 0f, Time.deltaTime / fadeOutTime);

            // Set the new volume to the AudioSource
            audioSource.volume = newVolume;
        }

        if (fadingOut && audioSource.volume <= 0)
        {
            fadingOut = false;
        }
    }

    public void FadeOut()
    {
        fadingOut = true;
    }
}