using System;
using UnityEngine;

public class LabTask : MonoBehaviour
{
    public MetricTable table;
    
    public string name;
    public string description;
    public bool done = false;
    
    public string internalName;
    public string internalLabName;

    public void SaveResultsToFile()
    {
        table.DumpCSV("username" + "_" + internalLabName + "_" + internalLabName + ".csv");
    }
}
