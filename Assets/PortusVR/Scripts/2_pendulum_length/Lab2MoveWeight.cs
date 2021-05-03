using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab2MoveWeight : MetricReading
{
    public Transform point_a;
    public Transform point_b;
    public Transform point_to_count_distance;
    public GameObject moving_point;
    public TextController textController;
    private FixedJoint fixedJoint;

    public Stopwatch stopwatch;

    // Start is called before the first frame update
    void Start()
    {
        fixedJoint = moving_point.GetComponent<FixedJoint>();
    }

    public override void ReadMetric()
    {
        var time = stopwatch.Time;
        var numSwings = 30;
        AddMetric("Длина (М)", Vector3.Distance(moving_point.transform.position, point_to_count_distance.position));
        AddMetric("Время (с)", time);
        AddMetric("Период (с)", time / numSwings, true);
        AddMetric("Частота (Гц)", (float)numSwings / time, true);
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
