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
    [SerializeField]
    private bool setShadersToFade = false;

    private Renderer[] renderers;
    private bool fadingIn = false;
    private bool fadingOut = false;
    private float time;


    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        if (setShadersToFade)
        {
            SetShadersToFade();
        }
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
            if (setShadersToFade)
            {
                SetShadersToOpaque();
            }
        }
        else if (fadingOut && opacity <= fadeOutOpacity)
        {
            fadingOut = false;
        }
    }

    private void SetShadersToFade()
    {
        SetShadersMode(true);
    }

    private void SetShadersToOpaque()
    {
        SetShadersMode(false);
    }

    private void SetShadersMode(bool isTransparent)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            var renderer = renderers[i];
            var materials = renderer.materials;

            for (var j = 0; j < materials.Length; j++)
            {
                if (!materials[j].HasInt("_Mode") || materials[j].shader.name != "Standard")
                {
                    break;
                }

                if (isTransparent)
                {
                    // Use the raw integer value for transparency blend mode
                    materials[j].SetInt("_Mode", 2); // 3 corresponds to the fade blend mode
                    materials[j].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    materials[j].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    materials[j].SetInt("_ZWrite", 0);
                    materials[j].DisableKeyword("_ALPHATEST_ON");
                    materials[j].EnableKeyword("_ALPHABLEND_ON");
                    materials[j].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    materials[j].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                }
                else
                {
                    // Set the material to opaque mode
                    materials[j].SetInt("_Mode", 0); // 0 corresponds to the opaque blend mode
                    materials[j].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    materials[j].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    materials[j].SetInt("_ZWrite", 1);
                    materials[j].DisableKeyword("_ALPHABLEND_ON");
                    materials[j].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    materials[j].DisableKeyword("_ALPHATEST_ON");
                    materials[j].renderQueue = -1; // Use default render queue for opaque materials
                }
            }
            renderers[i].materials = materials;
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
        if (setShadersToFade)
        {
            SetShadersToFade();
        }
        fadingOut = true;
        fadingIn = false;
        time = 0;
    }


    private void SetOpacity(float alpha) {
     for (int i = 0; i < renderers.Length; i++)
        {
            var renderer = renderers[i];
            var materials = renderer.materials;

            for (var j = 0; j < materials.Length; j++)
            {
                var material = materials[j];
                Color color = material.color;
                color.a = alpha;
                material.color = color;
                materials[j] = material;
            }
            renderers[i].materials = materials;
            
        }
    }



}


