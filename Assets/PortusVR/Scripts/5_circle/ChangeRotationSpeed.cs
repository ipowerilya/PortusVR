using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRotationSpeed : MonoBehaviour
{
    //public HingeJoint hinge_joint;
    public GameObject moving_point;
    private HingeJoint hinge_joint;
    // degrees per second
    public float MaxTargetVelocity;

    // Start is called before the first frame update
    void Start()
    {
        hinge_joint = moving_point.GetComponent<HingeJoint>();
        //UpdateRotationSpeed(0);
    }
    public void UpdateRotationSpeed(float percentage_raw)
    {
        float percentage = ((percentage_raw / 100f) - 0.5f) * 2; // change interval from [0;1] to [-1;1]
        var motor = hinge_joint.motor;
        
        motor.targetVelocity = percentage * MaxTargetVelocity;
        hinge_joint.motor = motor;
    }
}
