using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckIfCloseToHeadset : MonoBehaviour
{
    [SerializeField]
    private GameObject headset;
    [SerializeField]
    private BoxCollider boxCollider;
    [SerializeField]
    private float maxRange = 0;
    [SerializeField]
    private UnityEvent OnInRange;

    private bool checkDistance;

    void Update()
    {
        if (!checkDistance)
        {
            return;
        }
        Vector3 distance = headset.transform.position - boxCollider.bounds.center;

        if (distance.sqrMagnitude < maxRange)
        {
            OnInRange.Invoke();
            checkDistance = false;
        }

    }

    public void StartCheckDistance()
    {
        checkDistance = true;
    }
}
