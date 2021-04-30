using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConstantForce))]
public class ConstantRotation : MonoBehaviour
{
    public Transform axisPoint;
    public float forceMagnitude { get; set; } = 1f;

    Vector3 axis { get { return axisPoint.TransformVector(Vector3.up); } }
    Vector3 toAxis { get { return axisPoint.position - transform.position; } }

    Rigidbody rb;
    ConstantForce cf;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cf = gameObject.GetComponent<ConstantForce>();
    }

    public Vector3 GetAxisOrtho()
    {
        return toAxis - axis * (Vector3.Dot(toAxis, axis) / Vector3.Dot(axis, axis));
    }

    public Vector3 GetAxisTangent()
    {
        return Vector3.Cross(axis, toAxis);
    }

    private void FixedUpdate()
    {
        var tangent = GetAxisTangent();
        cf.force = tangent.normalized * forceMagnitude / (tangent.sqrMagnitude + 1);
    }
}
