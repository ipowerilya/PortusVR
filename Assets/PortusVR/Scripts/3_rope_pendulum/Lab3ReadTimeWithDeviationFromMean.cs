using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lab3ReadTimeWithDeviationFromMean : MetricReading
{
    public Stopwatch stopwatch;

    public override void ReadMetric()
    {
        var time = stopwatch.Time;
        AddMetric("Время (с)", time);
        var timeColumn = table.GetColumn("Время (с)");
        var mean = table.GetColumn("Время (с)").Average();

        var deviationKey = "отклонение от среднего (с)";
        var deviationColumn = table.GetColumn(deviationKey);
        deviationColumn.Clear();
        foreach (var deviation in (from t in timeColumn select t - mean))
        {
            AddMetric(deviationKey, deviation);
        }
    }
}
