using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using BNG;

public class MomentText : MonoBehaviour
{
    public JointSnapZone[] snapZones;
    public Transform pivot;
    public Text outputText;

    void FixedUpdate()
    {
        var moment = snapZones
            .Where(sz => sz.SnappedObjectJoint != null && sz.SnapToObject != null)
            .Select(sz => Vector3.Distance(sz.transform.position, pivot.position) * sz.SnappedObjectJoint.currentForce.magnitude)
            .Sum();
        outputText.text = moment.ToString("0.00") + " Н*м";
    }
}
