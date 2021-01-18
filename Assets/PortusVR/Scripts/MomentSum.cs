using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using BNG;

public class MomentSum : MonoBehaviour
{
    public JointSnapZone[] snapZones;
    public float moment;
    public Text outputText;

    void FixedUpdate()
    {
        moment = snapZones
            .Where(sz => sz.SnappedObjectJoint != null && sz.SnapToObject != null)
            .Select(sz => Vector3.Distance(sz.transform.position, this.transform.position) * sz.SnappedObjectJoint.currentForce.magnitude)
            .Sum();
        outputText.text = moment.ToString();
    }
}
