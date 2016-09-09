using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Seek : MonoBehaviour
{

    public Transform target;

    public float force = 10.0f;

    new private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //apply a force towards the target.
        var direction = (target.position - transform.position).normalized;
        rigidbody.AddForce(direction * force * Time.fixedDeltaTime, ForceMode.Force);
    }
}
