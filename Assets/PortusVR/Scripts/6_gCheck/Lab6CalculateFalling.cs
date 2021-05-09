using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab6CalculateFalling : MetricReading
{
    public Stopwatch stopwatch;
    public Transform top;
    public Transform bottom;

    public override void ReadMetric()
    {
        var time = stopwatch.Time;
        var height = Vector3.Distance(top.position, bottom.position);
        var accel = 2 * height / time / time;
        AddMetric("Время (с)", time);
        AddMetric("Высота (м)", height);
        AddMetric("Ускор. (м/с^2)", accel, true);
    }
}
