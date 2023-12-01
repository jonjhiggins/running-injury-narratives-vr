using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{

    public float fadeOutDuration = 1.3f;
    public float fadeInDuration = 1.3f;
    public float fadeOutOpacity = 0;
    public float fadeInOpacity = 1;
    public bool startFadedOut = true;

    public TMP_Text textMeshPro;

    void Awake()
    {
        if (textMeshPro == null)
        {
            textMeshPro = gameObject.GetComponent<TMP_Text>();
        }

        if (startFadedOut)
        {
            StartCoroutine(FadeRoutine(0f, true));

        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(fadeInDuration, false)); 
    }

    public void FadeOut()
    {
        StartCoroutine(FadeRoutine(fadeOutDuration, true));


    }

    IEnumerator FadeRoutine(float duration, bool fadeOut)
    {
        float elapsedTime = 0f;
        float targetOpacity = fadeOut ? fadeOutOpacity : fadeInOpacity;
        Color startColor = textMeshPro.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetOpacity);

        while (elapsedTime < duration)
        {
            textMeshPro.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the text is fully transparent at the end
        textMeshPro.color = targetColor;
    }
}
