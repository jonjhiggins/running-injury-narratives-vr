using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAndRainDumbbells : MonoBehaviour
{
    [SerializeField]
    private GameObject dumbbellGameObject;
    [SerializeField]
    private int numberOfDumbbells;
    [SerializeField]
    private float minScale;
    [SerializeField]
    private float maxScale;
    [SerializeField]
    private float minXPos;
    [SerializeField]
    private float maxXPos;
    [SerializeField]
    private float minYPos;
    [SerializeField]
    private float maxYPos;
    [SerializeField]
    private float minZPos;
    [SerializeField]
    private float maxZPos;
    [SerializeField]
    private float minXRotation;
    [SerializeField]
    private float maxXRotation;
    [SerializeField]
    private float minYRotation;
    [SerializeField]
    private float maxYRotation;
    [SerializeField]
    private float minZRotation;
    [SerializeField]
    private float maxZRotation;
    [SerializeField]
    private float forceAmount;
    [SerializeField]
    private float pauseBeforeFall = 1f;
    [SerializeField]
    private Material dumbbellMaterial;

    public void CloneDumbbellsAnimation()
    {
        for (int i = 0; i < numberOfDumbbells; i++)
        {
            StartCoroutine(CloneDumbellDelay(1 * i));
        }
    }

    private IEnumerator CloneDumbellDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        CloneDumbbell();
    }

    private Vector3 GetRandomPosition()
    {
        float randomXPos = Random.Range(minXPos, maxXPos);
        float randomYPos = Random.Range(minYPos, maxYPos);
        float randomZPos = Random.Range(minZPos, maxZPos);
        return new Vector3(randomXPos, randomYPos, randomZPos);
    }

    private Quaternion GetRandomRotation()
    {
        float randomXRotation = Random.Range(minXRotation, maxXRotation);
        float randomYRotation = Random.Range(minYRotation, maxYRotation);
        float randomZRotation = Random.Range(minZRotation, maxZRotation);
        return Quaternion.Euler(randomXRotation, randomYRotation, randomZRotation);
    }

    private void CloneDumbbell()
    {
        
        float randomScale = Random.Range(minScale, maxScale);
        Vector3 randomPosition = GetRandomPosition();
        Quaternion randomRotation = GetRandomRotation();   
        Vector3 clonedGameObjectPosition = dumbbellGameObject.transform.position + randomPosition;


        GameObject clonedGameObject = Instantiate(dumbbellGameObject.gameObject, clonedGameObjectPosition, randomRotation);
        clonedGameObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        
        // Change material
        Renderer clonedGameObjectRenderer = clonedGameObject.GetComponent<Renderer>();
        var materials = clonedGameObjectRenderer.materials;

        for (var i = 0; i < materials.Length; i++)
        {
            Debug.Log(materials[i]);
            materials[i] = dumbbellMaterial;
        }
        clonedGameObjectRenderer.materials = materials;

        StartCoroutine(AddForce(clonedGameObject));


    }


    private IEnumerator AddForce(GameObject clonedGameObject)
    {
        yield return new WaitForSeconds(pauseBeforeFall);
        Vector3 force = GetRandomPosition() * forceAmount;
        Rigidbody clonedGameObjectRigidBody = clonedGameObject.AddComponent<Rigidbody>();
        clonedGameObjectRigidBody.useGravity = true;
        //clonedGameObjectRigidBody.mass = 0.1f;
        clonedGameObjectRigidBody.drag = 10f;
        clonedGameObjectRigidBody.AddForce(force);
    }
}
