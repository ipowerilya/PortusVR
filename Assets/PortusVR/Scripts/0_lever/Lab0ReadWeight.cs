using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab0ReadWeight : MetricReading
{
    public Dynamometer dynamo;

    int GetWeightCount()
    {
        var snap = dynamo.snap;
        int count = 0;

        while (true)
        {
            var joint = snap.SnappedObjectJoint;
            if (joint != null)
            {
                snap = joint.connectedBody.gameObject.GetComponentInChildren<BNG.JointSnapZone>();
                if (snap != null) count += 1;
                else break;
            }
            else
            {
                break;
            }
        }

        return count;
    }

    public override void ReadMetric()
    {
        var count = GetWeightCount();
        if (count > 0)
        {
            AddMetric("Сила (Н)", dynamo.GetWeight());
            AddMetric("Количество грузов", count);
        }
    }
}
