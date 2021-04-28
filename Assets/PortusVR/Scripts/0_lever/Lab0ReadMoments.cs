using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab0ReadMoments : MetricReading
{
    public List<BNG.JointSnapZone> snaps; // first two are picked
    public Transform pivot;

    public override void ReadMetric()
    {
        BNG.JointSnapZone firstSnap = null;
        BNG.JointSnapZone secondSnap = null;
        
        foreach (var snap in snaps)
        {
            if (snap.SnappedObjectJoint != null)
            {
                if (firstSnap == null) firstSnap = snap;
                else if (secondSnap == null) secondSnap = snap;
                else break;
            }
        }

        if (firstSnap == null || secondSnap == null)
        {
            Debug.Log("No weights found on lever");
            return;
        }

        var L1 = Vector3.Distance(firstSnap.transform.position, pivot.position);
        var L2 = Vector3.Distance(secondSnap.transform.position, pivot.position);
        AddMetric("Плечо L1 (М)", L1);
        AddMetric("Плечо L2 (М)", L2);

        var F1 = firstSnap.SnappedObjectJoint.currentForce.magnitude;
        var F2 = secondSnap.SnappedObjectJoint.currentForce.magnitude;
        AddMetric("Сила F1 слева (Н)", F1);
        AddMetric("Сила F2 справа (Н)", F2);

        var fRatio = F1 / F2;
        var lRatio = L1 / L2;
        AddMetric("F1/F2", fRatio);
        AddMetric("L1/L2", lRatio);
        
        var M1 = F1 * L1;
        var M2 = F2 * L2;
        AddMetric("M1 (Н*М)", M1);
        AddMetric("M2 (Н*М)", M2);
    }
}
