using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAndRainDumbbells : MonoBehaviour
{
    [SerializeField]
    private GameObject sceneGameObject;
    [SerializeField]
    private GameObject dumbbellGameObject;
    private GameObject clonedGameObject;
    public void CloneDumbbell()
    {
        Vector3 clonedGameObjectPosition = dumbbellGameObject.transform.position + new Vector3(0, 5, 0);
        clonedGameObject = Instantiate(dumbbellGameObject.gameObject, clonedGameObjectPosition, dumbbellGameObject.transform.rotation);
        clonedGameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Rigidbody clonedGameObjectRigidBody = clonedGameObject.AddComponent<Rigidbody>();
        clonedGameObjectRigidBody.useGravity = true;
        clonedGameObjectRigidBody.mass = 0.1f;
        clonedGameObjectRigidBody.drag = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (clonedGameObject == null)
        {
            return;
        }
        clonedGameObject.transform.LookAt(dumbbellGameObject.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
       // transform.SetParent(sceneGameObject.transform.transform, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
    }
}
