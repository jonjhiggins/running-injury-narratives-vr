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

    private bool checkDistance = false;

    void Update()
    {
        if (!checkDistance)
        {
            return;
        }
        Vector3 distance = headset.transform.position - boxCollider.bounds.center;

        ScaleDistanceIndicator(distance);

        if (distance.sqrMagnitude < maxRange)
        {
            OnInRange.Invoke();
            checkDistance = false;
            distanceIndicator.SetActive(false);
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

    public void StartCheckDistance()
    {
        checkDistance = true;
    }
}
