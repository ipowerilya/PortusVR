using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamometer : MonoBehaviour
{
    public BNG.JointSnapZone snap;

    public float GetWeight()
    {
        var joint = snap.SnappedObjectJoint;
        var magnitude = joint != null ? joint.currentForce.magnitude : 0f;
        return magnitude;
    }
}
