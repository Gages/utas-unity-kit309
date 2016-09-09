using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SteeringController : MonoBehaviour {

    public float maxSpeed = 10.0f;
    public float maxTurnSpeed = 180.0f; // degrees per second
    public float maxForce = 10.0f;
    public Vector3 steerAccumulator = Vector3.zero;
    private Vector3 steerForce = Vector3.zero;

    new private Rigidbody rigidbody;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update () {
        RotateToHeading();
    }

    void FixedUpdate()
    {
         rigidbody.AddForce(steerForce);
    }

    void LateUpdate()
    {
        steerAccumulator.y = 0;
        steerForce = Vector3.ClampMagnitude(steerAccumulator, maxForce);
        steerAccumulator = Vector3.zero;
    }

    private void RotateToHeading()
    {
        if(rigidbody.velocity.magnitude > 0.1)
        {
            
           transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(rigidbody.velocity),
                maxTurnSpeed * Time.deltaTime);
           
        }
    }
}
