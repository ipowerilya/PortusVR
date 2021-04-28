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
        var decimalMult = Mathf.Pow(10, decimalPlaces);
        value = Mathf.Round(value * decimalMult) / decimalMult;
        if (taskManager == null)
        {
            Debug.Log("task manager not found, this should never happen");
            FindTaskManager();
        }
        taskManager.AddMetric(key, value);
    }
}
