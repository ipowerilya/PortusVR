using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_weight : MetricReading
{
    public Transform point_a;
    public Transform point_b;
    public Transform point_to_count_distance;
    public string metric_key;
    public GameObject moving_point;
    public TextController textController;
    private FixedJoint fixedJoint;

    // Start is called before the first frame update
    void Start()
    {
        fixedJoint = moving_point.GetComponent<FixedJoint>();
    }

    public override void ReadMetric()
    {
        AddMetric(metric_key, Vector3.Distance(moving_point.transform.position, point_to_count_distance.position));
    }

    public void ComputeAllMetrics()
    {
        var dist = Vector3.Distance(moving_point.transform.position, point_to_count_distance.position);
        AddMetric("Длинна (М)", dist);
        AddMetric("Время (с)", GetLastTimeInterval());
        AddMetric("Период (с)", GetLastTimeInterval() / 30);
        AddMetric("Частота (Гц)", 30 / GetLastTimeInterval());
    }

    public void UpdatePosition(float percentage)
    {
        // Debug.Log("percentage " + percentage);
        Vector3 new_position = Vector3.Lerp(point_a.position, point_b.position, percentage / 100f);
        // Debug.Log("position " + new_position);
        moving_point.transform.position = new_position;
        fixedJoint.connectedAnchor = new_position;
        textController.SetValue(Vector3.Distance(moving_point.transform.position, point_to_count_distance.position));
    }

    public void Freeze()
    {
        moving_point.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Release()
    {
        moving_point.GetComponent<Rigidbody>().isKinematic = false;
    }
}
