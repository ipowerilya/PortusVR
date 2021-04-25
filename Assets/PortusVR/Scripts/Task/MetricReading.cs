using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implement this abstract class for every task
public abstract class MetricReading : MonoBehaviour
{
    LabTasksManager taskManager;

    private void Start()
    {
        taskManager = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<LabTasksManager>();
    }

    // trigger this to call AddMetric
    public abstract void ReadMetric();

    // Use this for adding metrics
    public void AddMetric(string key, float value)
    {
        taskManager.AddMetric(key, value);
    }
}
