using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckIfCloseToHeadset : MonoBehaviour
{
    [SerializeField]
    private GameObject headset;
    [SerializeField]
    private GameObject distanceIndicator;
    [SerializeField]
    private BoxCollider boxCollider;
    [SerializeField]
    private float maxRange = 0;
    [SerializeField]
    private UnityEvent OnInRange;
    [SerializeField]
    private float waitDurationBeforeShowBringMeCloser = 5;
    [SerializeField]
    private GameObject bringMeCloser;

    private bool checkDistance = false;
    private bool checkDistanceHasBeenSet = false;
    private Coroutine DelayShowBringMeCloseRoutine;

    void Update()
    {
        if (!checkDistance)
        {
            return;
        }

        bool isCloseToHeadset = IsCloseToHeadset();
        
        if (isCloseToHeadset)
        {
            OnCloseToHeadset();
        }

    }

    private void ScaleDistanceIndicator(Vector3 distance)
    {
        float maxDistance = 0.3f;
        var scaleMultiply = 1 / maxDistance;

        Vector3 scaleDistanceIndicator = distanceIndicator.transform.localScale;
        scaleDistanceIndicator.x = scaleMultiply * distance.sqrMagnitude;
        scaleDistanceIndicator.y = scaleMultiply * distance.sqrMagnitude;
        distanceIndicator.transform.localScale = scaleDistanceIndicator;
    }

    // The select event on Pointable Unity Wrapper on 
    // athlete object is called everytime its picked up.
    // We only want to set checkDistance the first time it is picked up
    public void StartCheckDistance()
    {
        if (checkDistanceHasBeenSet)
        {
            return;
        }
        checkDistance = true;
        checkDistanceHasBeenSet = true;
        DelayShowBringMeCloseRoutine = StartCoroutine(DelayShowBringMeClose());
    }

    public bool IsCloseToHeadset()
    {
        Vector3 distance = headset.transform.position - boxCollider.bounds.center;
        ScaleDistanceIndicator(distance);
        return distance.sqrMagnitude < maxRange;
    }

    private IEnumerator DelayShowBringMeClose()
    {
        yield return new WaitForSeconds(waitDurationBeforeShowBringMeCloser);
        bringMeCloser.SetActive(true);

    }

    private void OnCloseToHeadset()
    {
        bringMeCloser.SetActive(false);
        StopCoroutine(DelayShowBringMeCloseRoutine);
        OnInRange.Invoke();
        checkDistance = false;
        distanceIndicator.SetActive(false);
    }
}
