using UnityEditor;
using UnityEngine;
public class CopyTransform : MonoBehaviour
{
    [MenuItem("GameObject/SetPose", false, -10)] // now it show on right click on the object
    public static void SetPose()
    {
        TransformData transformDataOfObj01; //TransformData is a class that store and transfer the transform from
        TransformData transformDataOfObj02; // one object to another

        Transform obj01 = Selection.activeTransform; // get the clicked object
        Transform obj02 = Instantiate(obj01, obj01.position, obj01.rotation); //duplicate it

        Transform[] transforms01 = obj01.GetComponentsInChildren<Transform>();
        Transform[] transforms02 = obj02.GetComponentsInChildren<Transform>();

        int transLength01 = transforms01.Length;
        int transLength02 = transforms02.Length;

        if (transLength01 != transLength02) return; //MUST BE the same character with the same rigged structure

        for (int i = 0; i <= transLength01 - 1; i++)
        {
            transformDataOfObj01 = new TransformData(transforms01[i].transform);
            transformDataOfObj01.ApplyTo(transforms02[i].transform);
        }

        DestroyImmediate(obj02.GetComponent<Animator>()); //destroy the animator of the copy
    }
}
