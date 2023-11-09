using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private enum Scenes {
        MerryGoRound
    }
    [SerializeField]
    private Scenes sceneToChangeTo;
    // Start is called before the first frame update
    public void Change()
    {
        SceneManager.LoadScene(sceneToChangeTo.ToString());
    }
}
