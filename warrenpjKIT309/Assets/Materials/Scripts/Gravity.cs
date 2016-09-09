using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour {

    public Transform target;

    public float gravity = 10.0f;

    new private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var direction = (target.position - transform.position).normalized;
        rigidbody.AddForce(direction * gravity * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
}
