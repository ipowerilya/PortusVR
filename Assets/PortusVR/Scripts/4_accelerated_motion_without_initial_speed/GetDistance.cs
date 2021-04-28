using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDistance : MonoBehaviour
{
    public Transform PointA, PointB;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        dist = Vector3.Distance(PointA.position, PointB.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
