using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeChildren : MonoBehaviour
{
    [SerializeField]
    private float fadeInDuration;
    [SerializeField]
    private float fadeOutDuration;
    [SerializeField]
    private float fadeInOpacity = 1;
    [SerializeField]
    private float fadeOutOpacity = 0;

    private Renderer[] renderers;
    private bool fadingIn = false;
    private bool fadingOut = false;
    private float time;


    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        SetOpacity(fadeOutOpacity);
    }

    void Update()
    {
        if (!fadingIn && !fadingOut)
        {
            return;
        }

        var fadeDuration = fadingOut ? fadeOutDuration : fadeInDuration;
        time += Time.deltaTime;
        float opacityChange = Mathf.Clamp01(time / fadeDuration);
        float opacity = fadingIn ? opacityChange : fadeInOpacity - opacityChange;
        SetOpacity(opacity);

        if (fadingIn && opacity >= fadeInOpacity)
        {
            fadingIn = false;
        }
        else if (fadingOut && opacity <= fadeOutOpacity)
        {
            fadingOut = false;
        }
    }

    public void FadeIn()
    {
        fadingIn = true;
        fadingOut = false;
        time = 0;
    }

    public void FadeOut()
    {
        fadingOut = true;
        fadingIn = false;
        time = 0;
    }


    private void SetOpacity(float alpha) {
     for (int i = 0; i < renderers.Length; i++)
        {
            var renderer = renderers[i];
            var material = renderer.material;

            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }



}


