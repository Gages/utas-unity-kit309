using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringController))]
public class Arrive : MonoBehaviour
{

    public Transform target;
    public float weight = 1.0f;

    private SteeringController steerController;

    void Start()
    {
        steerController = GetComponent<SteeringController>();
    }

    //Seek: constantly accelerate towards a target until the velocity reaches a maximum threshold.
    //Arrive: constantly 
    //Logic: assume the target is stationary.
    //When the distance to the target is at or below the stopping distance,
    //apply the stopping force in the vector away from the target, until
    //the distance to the target is small en
    //against the direction of motion, 
    //
    //Logic: if the distance toby applying a force against the direction of motion.
    //If the 
    void Update()
    {

    }

    void FixedUpdate()
    {

    }
}
