using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComputeDeviationFromMean : MetricReading
{
    public override void ReadMetric()
    {
        var mean = GetAllTimeIntervals().Average();
        foreach (var time in GetAllTimeIntervals())
        {
            AddMetric("отклонение от среднего (с)", mean - time);
        }
    }
}
