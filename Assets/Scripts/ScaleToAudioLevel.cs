using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToAudioLevel : MonoBehaviour
{
    public AudioSource source;
    public Vector3 minScale;
    public Vector3 maxScale;
    public float loudnessSensibility = 100;
    public float threshold = 0.1f;
    private int sampleWindow = 64;

    private void Update()
    {
        float loudness = GetLoudnessFromAudioClip(source.timeSamples, source.clip) * loudnessSensibility;
        if (loudness < threshold)
        {
            loudness = 0;
        }
        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }

    private float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
        }
}
