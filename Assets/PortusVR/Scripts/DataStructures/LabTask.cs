using System;
using UnityEngine;
using System.IO;

public class LabTask : MonoBehaviour
{
    public MetricTable table;

    [TextArea(1, 1)] public string taskName;
    [TextArea(15, 20)] public string description;
    public bool done = false;
    
    public string internalName; 
    string internalLabName;

    public void Initialize(string internalLabName)
    {
        this.internalLabName = internalLabName;
        if (table.ReadTableFromFile(internalLabName, internalName))
            done = true;
        table.Awake();
    }

    string GetResultsFileName()
    {
        return "username" + "_" + internalLabName + "_" + internalName + ".csv";
    }

    public void SaveResultsToFile()
    {
        table.DumpToFile(GetResultsFileName());
    }

    public void DeleteResults()
    {
        File.Delete(Application.persistentDataPath + "/" + GetResultsFileName());
        table.ClearData();
        done = false;
    }
}
