using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAwayChildObject : MonoBehaviour
{
    [SerializeField]
    private GameObject childObject;
    [SerializeField]
    private GameObject spawnContainer;
    [SerializeField]
    private AudioClip floatAwaySound;
    // Start is called before the first frame update
    public void FloatAway()
    {
        var newObject = Instantiate(childObject, childObject.transform.position, childObject.transform.rotation, spawnContainer.transform);
        PlayAudio(newObject);
        var moveObjectTowardsTarget = newObject.GetComponent<MoveObjectTowardsTarget>();
        if (moveObjectTowardsTarget != null )
        {
            moveObjectTowardsTarget.MoveTowardsTarget();
        }
        Destroy(gameObject);
    }

    private void PlayAudio(GameObject newObject)
    {
        if (floatAwaySound == null)
        {
            return;
        }
        var audioSource = newObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = newObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 1;
        audioSource.PlayOneShot(floatAwaySound);
    }
}
