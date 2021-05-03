using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab1ReadFriction : MetricReading
{
    public RemoteControllable rc;
    public Tower tower;

    public override void ReadMetric()
    {
        float weight = tower.baseObject.GetComponent<Rigidbody>().mass * (tower.height + 1);
        float area = rc.rotationState ? 4 * 2
                                      : 4 * 3;
        float force = rc.GetComponent<ConstantForce>().force.magnitude;

        AddMetric("Площадь (М*2)", area, true);
        AddMetric("Вес (кг)", weight);
        AddMetric("Сила (Н)", force);
    }
}
