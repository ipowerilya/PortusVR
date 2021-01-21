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
    public float Dist1, Dist2;
    public GameObject Snap1, Snap2;
    //public Rigidbody Object;

    void FixedUpdate()
    {
        if (Snap1 != null)
        {
            Dist1 = Vector3.Distance(Snap1.transform.position, this.transform.position);
            Dist2 = Vector3.Distance(Snap2.transform.position, this.transform.position);
            Debug.Log("Dist1: " + Dist1);
            Debug.Log("Dist2: " + Dist2);
        }
        moment = snapZones
            .Where(sz => sz.SnappedObjectJoint != null && sz.SnapToObject != null)
            .Select(sz => Vector3.Distance(sz.transform.position, this.transform.position) * sz.SnappedObjectJoint.currentForce.magnitude)
            .Sum();
        outputText.text = moment.ToString();
    }
}
