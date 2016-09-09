using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public static class FlockingBehaviourExtensions
{

    public static Vector3 Sum(this IEnumerable<Vector3> values)
    {
        return values.Aggregate((a, b) => a + b);
    }
}

public class FlockingBehaviour : MonoBehaviour {

	public float maxSpeed = 10.0f;
	public float maxForce = 10.0f;
	public float maxTurnSpeed = 180.0f; // degrees

	public float range = 5.0f; 
	public float alignWeight = 1.0f;
	public float cohesionWeight = 1.0f;
	public float separationWeight = 2.0f;

    public float seekWeight = 1.0f;
    public Transform seekTarget;

	private Vector3 steerAccumulator = Vector3.zero;
	private Vector3 steerForce = Vector3.zero;

	private List<GameObject> theFlock;
	//private List<GameObject> inRange;

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.magnitude > 0.1)
        {
            // calucalte the angle between motion and current heading
            float turnAngle = Mathf.Clamp(Vector3.Angle(transform.forward, rb.velocity), 0, maxTurnSpeed * Time.deltaTime);
            // calculate the direction that you need to turn
            turnAngle *= Mathf.Sign(Vector3.Dot(transform.right, rb.velocity));
            // turn		
            transform.Rotate(new Vector3(0, 1, 0), turnAngle);
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(steerForce);
    }

    public void StartFlocking(List<GameObject> flock, Transform target)
    {
        theFlock = flock;
        seekTarget = target;
        StartCoroutine(FlockUpdate());
    }

    IEnumerator FlockUpdate () {
        /*starting with an infinite loop, as is common for coroutines of this sort: */
        while (true){
            /*Next we find the boids that are in range. This will be used by all the flocking behaviours
             * and we don’t want to recalculate for each (note that it might be slightly more efficient
             * to setup a trigger and simply update this list using onCollisionEnter and onCollisionExit):
             * */
            var inRange = theFlock.Where((boid) =>
            {
                float separation = Vector3.Distance(transform.position, boid.transform.position);
                return separation < range && boid != this.gameObject;
            }).ToList();
            
            /*Next we explicitly call each of the behaviours (unfortunately, we would have to modify this
             * code to add new behaviours): 
             * */
            steerAccumulator = Vector3.zero;
            if (inRange.Count > 0)
            {
                steerAccumulator += cohesionWeight   * CohesionSteering(inRange);
                steerAccumulator += alignWeight      * AlignSteering(inRange);
                steerAccumulator += separationWeight * SeparateSteering(inRange);
                if (seekTarget != null)
                steerAccumulator += seekWeight       * SeekSteering(inRange);
            }

            // Limit steering force to maximum
            steerAccumulator.y = 0;
            steerForce = Vector3.ClampMagnitude(steerAccumulator, maxForce);

            /*Finally we yield for a short (random) time before commencing the next iteration:*/
            float waitTime = Random.Range(0.1f, 0.2f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private Vector3 ClampSteering(Vector3 desired)
    {
      //  return Vector3.ClampMagnitude(desired, maxSpeed) - GetComponent<Rigidbody>().velocity;
         // return desired - GetComponent<Rigidbody>().velocity;
        return desired;
    }

    private Vector3 CohesionSteering(List<GameObject> inRange)
    {
        var center = inRange.Select(boid => boid.transform.position).Sum() / inRange.Count;
        return ClampSteering(center - transform.position);
    }

    private Vector3 AlignSteering(List<GameObject> inRange)
    {
        var vel = inRange.Select(boid => boid.GetComponent<Rigidbody>().velocity).Sum() / inRange.Count;
        return ClampSteering(vel);
    }

    private Vector3 SeparateSteering(List<GameObject> inRange)
    {
        var desiredVel = inRange.Select(boid =>
        {
            var vec = transform.position - boid.transform.position;
            return vec.normalized / vec.sqrMagnitude;
        }).Sum();

       //return Vector3.ClampMagnitude(desiredVel, maxSpeed) - GetComponent<Rigidbody>().velocity;
        return ClampSteering(desiredVel);
    }

    private Vector3 SeekSteering(List<GameObject> inRange)
    {
        return ClampSteering((seekTarget.position - transform.position).normalized * maxSpeed);
    }
}
