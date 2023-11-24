using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRunningMenIntoGroup : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private GameObject[] objectsToMove;

    private bool allowMove = false;

    public void MoveTowardsTarget()
    {
        allowMove = true;
    }

    void Update()
    {
        if (!allowMove)
        {
            return;
        }
        Vector3 relativePos = target.position - transform.position;
        var distance = relativePos.sqrMagnitude;

        for (int i = 0; i < objectsToMove.Length; i++)
        {
            var objectTransform = objectsToMove[i].transform;
            objectTransform.position = Vector3.MoveTowards(objectTransform.position, target.position, Time.deltaTime * moveSpeed);
        }


        if (distance < 0.3)
        {
            allowMove = false;
        }
    }
}
