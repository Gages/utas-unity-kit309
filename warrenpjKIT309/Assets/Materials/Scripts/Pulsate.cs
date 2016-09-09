using UnityEngine;
using System.Collections;

public class Pulsate : MonoBehaviour {

    // Use this for initialization
    void Start () {
    
    }

    public float magnitude = 0.2f;
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        float s = 1 + (Mathf.Sin(speed * Time.time) - 0.5f) * magnitude;
        transform.localScale = new Vector3(s, s, s);
    }
}
