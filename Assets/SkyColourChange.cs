using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColourChange : MonoBehaviour
{
    [System.Serializable]
    public struct SkyBoxColours
    {
        public Color topColor;
        public Color middleColor;
        public Color bottomColor;
    }

    public SkyBoxColours startColours;
    public SkyBoxColours darkColours;
    public SkyBoxColours lightColours;
    public SkyBoxColours endColours;

    private void Start()
    {
        RenderSettings.skybox.SetColor("_BottomColor", startColours.bottomColor);
        RenderSettings.skybox.SetColor("_MiddleColor", startColours.middleColor);
        RenderSettings.skybox.SetColor("_TopColor", startColours.topColor);
    }

    public void FadeToDark()
    {
        FadeToColours(darkColours);
    }

    public void FadeToLight()
    {
        FadeToColours(lightColours);
    }

    public void FadeToEnd()
    {
        FadeToColours(endColours);

    }
    private void FadeToColours(SkyBoxColours endValues)
    {
        var currentColours = GetCurrentColours();
        StartCoroutine(LerpFunction(currentColours, endValues, 5));
    }

    private SkyBoxColours GetCurrentColours()
    {
        var colors = new SkyBoxColours();
        colors.bottomColor = RenderSettings.skybox.GetColor("_BottomColor");
        colors.middleColor = RenderSettings.skybox.GetColor("_MiddleColor");
        colors.topColor = RenderSettings.skybox.GetColor("_TopColor");
        return colors;
    }


    IEnumerator LerpFunction(SkyBoxColours startValues, SkyBoxColours endValues, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            RenderSettings.skybox.SetColor("_BottomColor", Color.Lerp(startValues.bottomColor, endValues.bottomColor, time / duration));
            RenderSettings.skybox.SetColor("_MiddleColor", Color.Lerp(startValues.middleColor, endValues.middleColor, time / duration));
            RenderSettings.skybox.SetColor("_TopColor", Color.Lerp(startValues.topColor, endValues.topColor, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
    }


}
