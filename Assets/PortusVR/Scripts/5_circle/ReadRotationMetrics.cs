using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadRotationMetrics : MetricReading
{
    public Stopwatch stopwatch;

    public override void ReadMetric()
    {
        var time = stopwatch.Time;
        AddMetric("Время (с)", time);

        // AddMetric("Сила (Н)", force);
        // AddMetric("Радиус (м)", radius);
        // AddMetric("Центростремительное ускорение (М/c*2)", accel);
        // AddMetric("Сила упругости (Н)", tension);
    }
}
