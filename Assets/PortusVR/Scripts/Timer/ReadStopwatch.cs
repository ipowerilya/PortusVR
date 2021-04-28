using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadStopwatch : MetricReading
{
    public Stopwatch stopwatch;

    public override void ReadMetric()
    {
        AddMetric("Время (с)", stopwatch.Time);
    }
}
