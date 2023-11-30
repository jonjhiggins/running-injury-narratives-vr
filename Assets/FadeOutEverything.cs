using System.Collections;
using UnityEngine;

public class FadeOutEverything : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    public GameObject ovrCameraRig;

    public void Begin()
    {
        StartCoroutine(FadeOut());
    }

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // Get all GameObjects with Renderer components in the scene excluding those under OVRCameraRig
        Renderer[] renderers = GetFilteredRenderers();

        // Use Mathf.Lerp to smoothly interpolate between start and target alpha over time
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            foreach (Renderer renderer in renderers)
            {
                // Get the initial alpha value
                float startAlpha = renderer.material.color.a;

                // Define the target alpha value (fully transparent)
                float targetAlpha = 0.0f;

                // Calculate the new alpha value
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);

                // Update the material's color with the new alpha value
                Color newColor = renderer.material.color;
                newColor.a = newAlpha;
                renderer.material.color = newColor;
            }

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final alpha value is set to avoid rounding errors
        foreach (Renderer renderer in renderers)
        {
            Color finalColor = renderer.material.color;
            finalColor.a = 0.0f;
            renderer.material.color = finalColor;
        }
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