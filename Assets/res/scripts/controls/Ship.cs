using UnityEngine;

public class Ship : MonoBehaviour
{
    public float forwardSpeed;
    public float backwardSpeed;
    public float bankSpeed;
    public float tiltSpeed;
    protected Rigidbody rigidBody;

    void Start(){
        rigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate(){
        if (Input.GetKey(KeyCode.W)){
            rigidBody.AddRelativeForce(Vector3.forward * forwardSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)){
            rigidBody.AddRelativeForce(Vector3.back * backwardSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            rigidBody.AddRelativeTorque(Vector3.forward * bankSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            rigidBody.AddRelativeTorque(Vector3.back * bankSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            rigidBody.AddRelativeTorque(Vector3.right * tiltSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            rigidBody.AddRelativeTorque(Vector3.left * tiltSpeed * Time.deltaTime);
        }
    }
}
