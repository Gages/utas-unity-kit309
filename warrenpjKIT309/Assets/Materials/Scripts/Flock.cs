using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flock : MonoBehaviour
{
    public int flockSize = 10;
    public GameObject boidPrefab;
    public List<GameObject> flock;
    public Transform target;

    void Start()
    {
        flock = new List<GameObject>();
        for (var i = 0; i < flockSize; i++)
        {
            GameObject boid = (GameObject)GameObject.Instantiate(boidPrefab, transform.position, transform.rotation);
            boid.transform.parent = transform;
            Vector2 pos = Random.insideUnitCircle * GetComponent<Collider>().bounds.size.x / 2.0f;
            Vector3 position = new Vector3(pos.x, 0, pos.y);
            boid.transform.localPosition = position;
            flock.Add(boid);
        }

        foreach (var boid in flock)
        {
            FlockingBehaviour behaviour = boid.GetComponent<FlockingBehaviour>();
            if (behaviour != null)
            {
                behaviour.StartFlocking(flock, target);
            }
        }
    }
}