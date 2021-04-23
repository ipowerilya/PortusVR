using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTest : MonoBehaviour
{
    public MetricTable table;
    public void record()
    {
        table.AddMetric("A", Random.Range(0, 1000));
        table.AddMetric("B", Random.Range(0, 1000));
    }

    public void RecordByTag(string TagName)
    {
        table.AddMetric(TagName, Random.Range(0,1000));
    }

    public void DumpLabOnlyByLabName(string lab_name)
    {
        table.DumpLab(lab_name, "default");
    }
}
