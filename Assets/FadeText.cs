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

    public TMP_Text textMesh;

    void Start()
    {
        if (textMesh == null)
        {
            textMesh = gameObject.GetComponent<TMP_Text>();
        }
    }

    public void FadeIn()
    {
        textMesh.CrossFadeAlpha(fadeInOpacity, fadeOutDuration, false);
    }

    public void FadeOut()
    {
        textMesh.CrossFadeAlpha(fadeOutOpacity, fadeOutDuration, false);
    }
}
