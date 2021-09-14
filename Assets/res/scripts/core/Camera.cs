using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos =  target.transform.position;
        
        
        Vector3 vec = new Vector3(0, 3, -10);
        pos += (target.transform.rotation.normalized * vec);
        transform.position = pos;
        transform.eulerAngles = target.transform.eulerAngles;
    }
}
