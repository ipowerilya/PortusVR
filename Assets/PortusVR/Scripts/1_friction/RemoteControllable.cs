using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConstantForce))]
public class RemoteControllable : MonoBehaviour {
    ConstantForce cf;
    public Vector3 maxForce = new Vector3(1, 0, 0);
    public FloatEvent onForceChange;

    Rigidbody rb;
    public Vector3 rotationStateA = Vector3.zero;
    public Vector3 rotationStateB = Vector3.right * 90;

    Vector3 position;
    
    public void Awake() {
        cf = GetComponent<ConstantForce>();
        cf.force = Vector3.zero;

        rb = GetComponent<Rigidbody>();
        position = transform.localPosition;
    }
    public void SetForce(Vector3 newForce) {
        cf.force = newForce;
        onForceChange.Invoke(newForce.magnitude);
    }

    public void SetForcePercentage(float percentage) {
        SetForce((maxForce * (percentage - 50f)*2f)/100f);
    }

    public void SetRotationState(bool rotationState) {
        var rotation = rotationState ? rotationStateB : rotationStateA;
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void SetRotationA() {
        SetRotationState(false);
    }

    public void SetRotationB() {
        SetRotationState(true);
    }

    public void ResetState() {
        transform.localPosition = position;
        cf.force = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
