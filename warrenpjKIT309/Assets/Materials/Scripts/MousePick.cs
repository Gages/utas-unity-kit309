using UnityEngine;
using System.Collections;

public class MousePick : MonoBehaviour {

    // Use this for initialization
    void Start () {
    
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                transform.position = new Vector3(hitinfo.point.x, 0.5f, hitinfo.point.z);
            }
        }
    }
}
