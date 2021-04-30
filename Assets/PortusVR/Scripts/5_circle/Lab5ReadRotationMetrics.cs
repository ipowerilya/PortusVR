using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab5ReadRotationMetrics : MetricReading
{
    public ConstantRotation cr;
    public Stopwatch stopwatch;

    public override void ReadMetric()
    {
        var time = stopwatch.Time / 30.0f;
        var radius = cr.GetAxisOrtho().magnitude;
        var accel = 4 * Mathf.PI * Mathf.PI * radius / (time * time);
        var mass = cr.GetComponent<Rigidbody>().mass;
        var centrifugalForce = cr.GetAxisOrtho().normalized * mass * accel;
        var mg = Physics.gravity * mass;
        var tension = centrifugalForce - mg;

        AddMetric("Время (с)", time);
        AddMetric("Радиус (м)", radius);
        AddMetric("Центр.уск.(М/c*2)", accel);
        AddMetric("Сила (Н)", centrifugalForce.magnitude);
        AddMetric("Сила упр. (Н)", tension.magnitude);
    }
}
