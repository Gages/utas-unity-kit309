using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringController))]
public class SteeringBehaviour : MonoBehaviour {

    protected SteeringController steerController;
    new protected Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        steerController = GetComponent<SteeringController>();
        rigidbody = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
