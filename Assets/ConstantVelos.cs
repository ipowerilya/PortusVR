using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVelos : MonoBehaviour
{
    public Rigidbody rb;
    public float ConstantVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0,0, ConstantVelocity);
    }
}
