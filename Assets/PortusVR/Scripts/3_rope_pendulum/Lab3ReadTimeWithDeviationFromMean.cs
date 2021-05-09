using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lab3ReadTimeWithDeviationFromMean : MetricReading
{
    public Stopwatch stopwatch;
    public Transform ball;
    public Transform ceiling;

    public override void ReadMetric()
    {
        var time = stopwatch.Time;
        AddMetric("Время (с)", time);
        var timeColumn = table.GetConstColumn("Время (с)");
        var mean = table.GetConstColumn("Время (с)").Average();

        var deviationKey = "отклонение от среднего (с)";
        var deviationColumn = table.GetConstColumn(deviationKey);
        for (int index = 0;  index < deviationColumn.Count; ++index)
        {
            AddMetricByIndex(deviationKey, index, timeColumn[index] - mean, true);
        }

        var length = Vector3.Distance(ball.position, ceiling.position);
        var period = time / 10;
        var accel = 4 * Mathf.PI * Mathf.PI * length / period / period;

        AddMetric("Длина (м)", length);
        AddMetric("Ускор. (м/с^2)", accel, true);
    }
}
