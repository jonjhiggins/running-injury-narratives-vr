using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAwayChildObject : MonoBehaviour
{
    [SerializeField]
    private GameObject childObject;
    [SerializeField]
    private GameObject spawnContainer;
    // Start is called before the first frame update
    public void FloatAway()
    {
        var newObject = Instantiate(childObject, childObject.transform.position, childObject.transform.rotation, spawnContainer.transform);
        var moveObjectTowardsTarget = newObject.GetComponent<MoveObjectTowardsTarget>();
        if (moveObjectTowardsTarget != null )
        {
            moveObjectTowardsTarget.MoveTowardsTarget();
        }
        Destroy(gameObject);
    }

}
