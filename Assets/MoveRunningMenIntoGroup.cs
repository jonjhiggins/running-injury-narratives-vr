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

        float distance = 0;

        for (int i = 0; i < objectsToMove.Length; i++)
        {
            var objectTransform = objectsToMove[i].transform;
            objectTransform.position = Vector3.MoveTowards(objectTransform.position, target.position, Time.deltaTime * moveSpeed);

            Vector3 relativePos = target.position - objectTransform.position;
            distance += relativePos.sqrMagnitude;
        }

        if (distance < 0.01)
        {
            allowMove = false;
        }

    }
}
