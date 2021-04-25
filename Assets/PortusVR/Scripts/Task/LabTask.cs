using System;
using UnityEngine;

public class LabTask : MonoBehaviour
{
    public MetricTable table;
    
    public string taskName;
    public string description;
    public bool done = false;
    
    public string internalName; 
    string internalLabName;

    public void Initialize(string internalLabName)
    {
        this.internalLabName = internalLabName;
        FillMetricTableFromFile.GetTable(internalLabName, internalName, table);
    }

    public void SaveResultsToFile()
    {
        table.DumpCSV("username" + "_" + internalLabName + "_" + internalLabName + ".csv");
    }
}
