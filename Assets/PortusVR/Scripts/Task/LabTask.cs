using System;
using UnityEngine;

public class LabTask : MonoBehaviour
{
    public MetricTable table;
    public string taskDescription;
    public string taskName;
    public string internalTaskName;
    public string internalLabName;
    public bool done = false;

    public void SaveResultsToFile()
    {
        table.DumpCSV("username" + "_" + internalLabName + "_" + internalLabName + ".csv");
    }
}
