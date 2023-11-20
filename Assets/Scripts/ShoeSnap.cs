using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeSnap : MonoBehaviour
{
    [SerializeField]
    private GameObject originalShoe;
    [SerializeField]
    private GameObject snappedShoe;
    [SerializeField]
    private GameObject spawnContainer;
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private float explosionRadius;

    public void Snap()
    {
        Destroy(originalShoe);
        gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        var newSnappedShoe = Instantiate(snappedShoe, snappedShoe.transform.position, snappedShoe.transform.rotation, spawnContainer.transform);
        newSnappedShoe.SetActive(true);
        var rigidbodies = snappedShoe.GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.AddExplosionForce(explosionForce, snappedShoe.transform.position, explosionRadius);
        }
    }

    
}
