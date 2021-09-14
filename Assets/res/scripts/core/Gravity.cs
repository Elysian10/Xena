using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float startDistance;
    public float midDistance;
    public float endDistance;
    public float startStrength;
    public float midStrength;
    public float endStrength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float startToMidDist = midDistance - startDistance;
        float midToEndDist = endDistance - midDistance;
        float startToMidStr = midStrength - startStrength;
        float midToEndStr = endStrength - midStrength;
        foreach (GameObject obj in Utils.actorList){
            Rigidbody rigitbody = (Rigidbody) obj.GetComponent(typeof(Rigidbody));
            Vector3 diff = transform.position - obj.transform.position;
            float dist = diff.magnitude;
            if (dist > endDistance || dist < startDistance)
                continue;
            float strength = 0;
            if (dist < midDistance){
                float factor = (dist - startDistance) / startToMidDist;
                strength = startToMidStr * factor;

            }
            else{
                float factor = (dist - midDistance) / midToEndDist;
                strength = midToEndDist * factor;

            }
            rigitbody.AddForce(diff.normalized * strength * Time.deltaTime);
        }
    }
}
