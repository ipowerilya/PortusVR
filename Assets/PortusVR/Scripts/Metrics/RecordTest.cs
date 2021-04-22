using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTest : MonoBehaviour
{
    public MetricTable table;
    public void record()
    {
        table.AddMetric("velocity", 1337f);
        table.AddMetric("time", 123123f);
    }
}
