using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainAthleteObjects : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> athleteObjects = new List<GameObject>();
    [SerializeField]
    private float waveDurationMin = 1;
    [SerializeField]
    private float waveDurationMax = 10;
    [SerializeField]
    private int numberOfObjectsPerWave = 2;

    [SerializeField]
    private float xPosMin;
    [SerializeField]
    private float xPosMax;
    [SerializeField]
    private float zPosMin;
    [SerializeField]
    private float zPosMax;

    private bool allowRain;

    private void Start()
    {
        if (allowRain)
        {
            RainWave();
        }
    }

    public void StartRain()
    {
        allowRain = true;
        RainWave();
    }

    public void StopRain()
    {
        allowRain = false;
    }



    private IEnumerator DelayedRainWave()
    {
        yield return new WaitForSeconds(GetRandomWaveDuration());
        RainWave();
    }
    private void RainWave()
    {
        for (int i = 0; i < numberOfObjectsPerWave; i++)
        {
            var newPosition = GetRandomPosition();
            var newRotation = GetRandomRotation();
            var randomAthleteObject = GetRandomAthleteObject();
            var newObject = Instantiate(randomAthleteObject, gameObject.transform, true);
            newObject.AddComponent<BoxCollider>();
            var rb = newObject.AddComponent<Rigidbody>();
            rb.mass = 0.01f;
            rb.drag = 1f;
            rb.AddTorque(GetRandomPosition());
            newObject.transform.localPosition = newPosition;
            newObject.transform.localRotation = newRotation;
            newObject.SetActive(true);
        }
        if (allowRain)
        {
            StartCoroutine(DelayedRainWave());
        }
    }

    private Vector3 GetRandomPosition()
    {
        var randomX = Random.Range(xPosMin, xPosMax);
        var randomZ = Random.Range(zPosMin, zPosMax);
        return new Vector3(randomX, 0, randomZ);
    }

    private Quaternion GetRandomRotation()
    {
        var randomX = Random.Range(0, 360);
        var randomY = Random.Range(0, 360);
        var randomZ = Random.Range(0, 360);
        return Quaternion.Euler(randomX, randomY, randomZ);
    }

    private GameObject GetRandomAthleteObject()
    {
        var randomIndex = Random.Range(0, athleteObjects.Count - 1);
        return athleteObjects[randomIndex];
    }
    private float GetRandomWaveDuration()
    {
        return Random.Range(waveDurationMin, waveDurationMax);
    }



}
