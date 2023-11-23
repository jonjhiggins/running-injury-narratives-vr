using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMedal : MonoBehaviour
{
    [SerializeField]
    private GameObject medalGold;
    [SerializeField]
    private GameObject spawnContainer;

    private GameObject newMedal;


    public void Clone()
    {
        GameObject medalWrapper = Instantiate(new GameObject(), transform.position, transform.rotation, spawnContainer.transform);
        
        newMedal = Instantiate(medalGold, medalWrapper.transform);
        newMedal.SetActive(true);
        Animation anim = newMedal.GetComponent<Animation>();
        float animLength = anim.clip.length + 0.1f;
        StartCoroutine(FloatTowardsTarget(animLength));
    }

    private IEnumerator FloatTowardsTarget(float delay)
    {
        yield return new WaitForSeconds(delay);
        var moveObjectTowardsTarget = newMedal.GetComponent<MoveObjectTowardsTarget>();
        moveObjectTowardsTarget.MoveTowardsTarget();
    }
}
