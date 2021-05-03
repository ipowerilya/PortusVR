using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implement this abstract class for every task
public abstract class MetricReading : MonoBehaviour
{
    public int decimalPlaces = 2;
    LabTasksManager taskManager;

    public MetricTable table 
    { 
        get { return taskManager.GetCurrentTask().table; } 
    }

    private void Start()
    {
        FindTaskManager();
    }

    private void FindTaskManager()
    {
        taskManager = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<LabTasksManager>();
    }

    // trigger this to call AddMetric
    public abstract void ReadMetric();

    // Use this for adding metrics
    public void AddMetric(string key, float value)
    {
        value = RoundAndFindManagerIfNeeded(value);
        taskManager.AddMetric(key, value);
    }

    public void AddMetricByIndex(string key, int index, float value)
    {
        value = RoundAndFindManagerIfNeeded(value);
        taskManager.AddMetricByIndex(key, index, value);
    }

    float RoundAndFindManagerIfNeeded(float value)
    {
        if (taskManager == null)
        {
            Debug.Log("task manager not found, this should never happen");
            FindTaskManager();
        }
        var decimalMult = Mathf.Pow(10, decimalPlaces);
        return Mathf.Round(value * decimalMult) / decimalMult;
    }
}
