using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloatUpMedalsHideOthers : MonoBehaviour
{
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float duration = 3f;
    [SerializeField]
    private float moveSpeed = 0.25f;



    private string medalTagName = "lowPolyMedal";
    private Transform[] objectsToMove;
    private bool fadingOut = false;
    private float fadeOutOpacity = 0;
    private float time = 0;
    private Renderer[] renderers;



    public void Begin ()
    {
        if (container == null)
        {
            return;
        }

        objectsToMove = GetChildGameObjectsWithTag(medalTagName);
        renderers = gameObject.GetComponentsInChildren<Renderer>();
        var children = gameObject.GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].tag != medalTagName)
            {
                break;
            }
            var rigidBody = children[i].gameObject.GetComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            var rigidBody = objectsToMove[i].gameObject.GetComponent<Rigidbody>();
            var moveObjectTowardsTarget = objectsToMove[i].gameObject.AddComponent<MoveObjectTowardsTarget>();
            rigidBody.useGravity = false;
            moveObjectTowardsTarget.target = target;
            moveObjectTowardsTarget.moveSpeed = moveSpeed;
            moveObjectTowardsTarget.rotateSpeed = 0f;
            moveObjectTowardsTarget.slowMovementWhenDistance = 0.01f;
            moveObjectTowardsTarget.MoveTowardsTarget();
        }
        fadingOut = true;
        StartCoroutine(DelayHide());

    }

    void Update()
    {
        if (!fadingOut)
        {
            return;
        }

        time += Time.deltaTime;
        float opacityChange = Mathf.Clamp01(time / duration);
        float opacity = 1 - opacityChange;
        SetOpacity(opacity);

        if (fadingOut && opacity <= fadeOutOpacity)
        {
            fadingOut = false;
        }
    }

    private IEnumerator DelayHide()
    {
        yield return new WaitForSeconds(duration);
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            objectsToMove[i].gameObject.SetActive(false);
        }
    }



    private Transform[] GetChildGameObjectsWithTag(string tag)
    {
        var transforms = transform.GetComponentsInChildren<Transform>();
        return transforms.Where(transform => transform.tag == tag).ToArray();
    }

    private void SetOpacity(float alpha)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            var renderer = renderers[i];
            var materials = renderer.materials;

            for (var j = 0; j < materials.Length; j++)
            {
                var material = materials[j];
                Color color = material.color;
                color.a = alpha;
                material.color = color;
                materials[j] = material;
            }
            renderers[i].materials = materials;

        }
    }
}
