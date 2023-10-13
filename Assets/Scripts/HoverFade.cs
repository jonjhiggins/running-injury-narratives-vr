using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverFade : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material hoverMaterial;
    private Material originalMaterial;

    private void Start()
    {
        originalMaterial = meshRenderer.material;
    }

    public void OnHover()
    {
        meshRenderer.material = hoverMaterial;
    }

    public void OnUnhover()
    {
        meshRenderer.material = originalMaterial;
    }
}
