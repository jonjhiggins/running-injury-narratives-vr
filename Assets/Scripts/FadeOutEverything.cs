using System.Collections;
using UnityEngine;

public class FadeOutEverything : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    public GameObject ovrCameraRig;

    private Renderer[] renderers;

    public void Begin()
    {
        renderers = GetFilteredRenderers();
        SetShaderToFade();
        StartCoroutine(FadeOut());
    }

    private void SetShaderToFade()
    {
        for (var r = 0; r < renderers.Length; r++)
            
        {
            var materials = renderers[r].materials;
            for (var i = 0; i < materials.Length; i++)
            {
                var material = materials[i];
                materials[i] = SetMaterialToFadeMode(material);
            }
            renderers[r].materials = materials;
        }
    }

    private Material SetMaterialToFadeMode(Material material)
    {
        if (material != null)
        {
            material.SetInt("_Mode", 2);
            // Set the rendering mode to "Fade"
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }
        return material;
    }

    IEnumerator FadeOut()
    {

        // Use Mathf.Lerp to smoothly interpolate between start and target alpha over time
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            foreach (Renderer renderer in renderers)
            {
                var materials = renderer.materials;
                for (var i = 0; i < materials.Length; i++)
                {
                    var material = materials[i];
                    var newAlpha = GetAlphaValue(material, elapsedTime);
                    materials[i] = SetAlphaOnMaterial(material, newAlpha);
                }
                renderer.materials = materials;
            }

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final alpha value is set to avoid rounding errors
        foreach (Renderer renderer in renderers)
        {
            var materials = renderer.materials;
            for (var i = 0; i < materials.Length; i++)
            {
                var material = materials[i];
                materials[i] = SetAlphaOnMaterial(material, 0f);
            }
            renderer.materials = materials;
        }
    }

    private float GetAlphaValue(Material material, float elapsedTime)
    {
        // Get the initial alpha value
        float startAlpha = material.color.a;

        // Define the target alpha value (fully transparent)
        float targetAlpha = 0.0f;

        // Calculate the new alpha value
        return Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
    }

    Material SetAlphaOnMaterial(Material material, float alpha)
    {
        Color finalColor = material.color;
        finalColor.a = alpha;
        material.color = finalColor;
        return material;
    }

    Renderer[] GetFilteredRenderers()
    {
        // Get all GameObjects with Renderer components in the scene
        Renderer[] allRenderers = UnityEngine.Object.FindObjectsOfType<Renderer>();

        // Filter out renderers that are children of OVRCameraRig
        return System.Array.FindAll(allRenderers, renderer => !IsChildOfOVRCameraRig(renderer.transform));
    }

    bool IsChildOfOVRCameraRig(Transform objTransform)
    {
        // Check if the object's transform is a child of the OVRCameraRig
        return objTransform.IsChildOf(ovrCameraRig.transform);
    }
}