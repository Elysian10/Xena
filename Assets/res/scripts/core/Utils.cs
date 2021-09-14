using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static List<GameObject> actorList = new List<GameObject>();


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("Before scene loaded");
    }

[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoad(){
        Debug.Log("After scene loaded");
        //GameObject[] objArray = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        GameObject[] objArray = GameObject.FindGameObjectsWithTag("Rigid");
        for (int i = 0; i < objArray.Length; i++){
            if (objArray[i].GetComponent(typeof(Rigidbody)) != null)
                actorList.Add(objArray[i]);
        }
    }

}
