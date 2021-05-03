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
        var timeColumn = table.GetConstColumn("Время (с)");
        var mean = table.GetConstColumn("Время (с)").Average();

        var deviationKey = "отклонение от среднего (с)";
        var deviationColumn = table.GetConstColumn(deviationKey);
        for (int index = 0;  index < deviationColumn.Count; ++index)
        {
            AddMetricByIndex(deviationKey, index, timeColumn[index] - mean);
        }
    }
}
