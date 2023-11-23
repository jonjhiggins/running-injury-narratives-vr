using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveObjectTowardsTarget : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 10;
    public float rotateSpeed = 1;
    public float slowMovementWhenDistance = 0.5f;

    private Rigidbody _rigidbody;
    private bool allowMove = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    public void MoveTowardsTarget() { 
        allowMove = true;
    }

    void FixedUpdate()
    {
        if (!allowMove)
        {
            return;
        }
        Vector3 relativePos = target.position - transform.position;
        var distance = relativePos.sqrMagnitude;
        var rotateSpeedWithVelocity = rotateSpeed * distance;

        // Rotate the object
        _rigidbody.AddTorque(Vector3.up * rotateSpeedWithVelocity);
        _rigidbody.AddTorque(Vector3.right * rotateSpeedWithVelocity);

        // Move towards target
        _rigidbody.AddForce(relativePos * moveSpeed * Time.fixedDeltaTime);


        if (distance < slowMovementWhenDistance)
        {
            // Slow down movement
            _rigidbody.velocity = _rigidbody.velocity / 1.005f;
        }

        if (distance < slowMovementWhenDistance && _rigidbody.velocity.magnitude <= 0.1f)
        {
            // Stop movement
            _rigidbody.velocity = Vector3.zero;
            allowMove = false;
        }
    }
}
