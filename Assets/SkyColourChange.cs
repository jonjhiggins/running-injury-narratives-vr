using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColourChange : MonoBehaviour
{

    public Color startColor = new Color(0.5f, 0, 0, 1);
    public Color darkColor = new Color(0.5f, 0, 0, 1);
    public Color lightColor = new Color(0.5f, 0, 0, 1);

    private void Start()
    {
        RenderSettings.skybox.SetColor("_Tint", startColor);
    }

    public void FadeToDark()
    {
        StartCoroutine(LerpFunction(darkColor, 5));
    }

    public void FadeToLight()
    {
        StartCoroutine(LerpFunction(lightColor, 5));
    }


    IEnumerator LerpFunction(Color endValue, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(startColor, endValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        RenderSettings.skybox.SetColor("_Tint", endValue);
    }


}
