using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class HighlightInforgraphicAthletes : MonoBehaviour
{

    [SerializeField]
    private Transform[] inforgraphicAthletes;
    [SerializeField]
    private float highlightDelay = 0.6f;

    private Coroutine highlightRoutine;
    bool highlighting = false;

    private void Start()
    {
        inforgraphicAthletes = GetChildGameObjectsWithTag("infographicAthletes");
        Debug.Log(inforgraphicAthletes.Length);
    }


    private Transform[] GetChildGameObjectsWithTag(string tag)
    {
        var transforms = transform.GetComponentsInChildren<Transform>();
        return transforms.Where(transform => transform.tag == tag).ToArray();
    }

    public void HighlightAthletes()
    {
        highlightRoutine = StartCoroutine(DelayedHighlight());
        highlighting = true;
    }

    public void StopHighlightAthletes()
    {
        StopCoroutine(highlightRoutine);
        highlighting = false;
    }

    private IEnumerator DelayedHighlight()
    {
        var index = Random.Range(0, inforgraphicAthletes.Length);
        var animation = inforgraphicAthletes[index].gameObject.GetComponent<Animation>();
        animation.Play();
        yield return new WaitForSeconds(highlightDelay);
        if (highlighting)
        {
            StartCoroutine(DelayedHighlight());
        }
    }
}
