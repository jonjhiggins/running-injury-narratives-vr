using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBetweenScenes : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("keepBetweenScenes");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
