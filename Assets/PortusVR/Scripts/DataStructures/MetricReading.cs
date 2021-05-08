using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implement this abstract class for every task
public abstract class MetricReading : MonoBehaviour
{
    public int decimalPlaces = 2;
    bool enabledAutoComputation { get { return GetTaskManager().lab.enabledAutoComputation; } }

    public MetricTable table 
    { 
        get { return GetTaskManager().GetCurrentTask().table; } 
    }

    private LabTasksManager GetTaskManager()
    {
        return GameObject.FindGameObjectsWithTag("TaskManager")[0].GetComponent<LabTasksManager>();
    }

    // trigger this to call AddMetric
    public abstract void ReadMetric();

    // Use this for adding metrics
    public void AddMetric(string key, float value, bool isAutoComputedValue = false)
    {
        if (isAutoComputedValue && !enabledAutoComputation)
            return;
        value = RoundAndFindManagerIfNeeded(value);
        GetTaskManager().AddMetric(key, value);
    }

    public void AddMetricByIndex(string key, int index, float value, bool isAutoComputedValue = false)
    {
        if (isAutoComputedValue && !enabledAutoComputation)
            return;
        value = RoundAndFindManagerIfNeeded(value);
        GetTaskManager().AddMetricByIndex(key, index, value);
    }

    float RoundAndFindManagerIfNeeded(float value)
    {
        var decimalMult = Mathf.Pow(10, decimalPlaces);
        return Mathf.Round(value * decimalMult) / decimalMult;
    }
}
