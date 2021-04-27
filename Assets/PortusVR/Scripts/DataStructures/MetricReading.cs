using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implement this abstract class for every task
public abstract class MetricReading : MonoBehaviour
{
    public int decimalPlaces = 2;
    LabTasksManager taskManager;

    //for easy connection with timer
    List<float> time = new List<float>();

    private void Start()
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
            Debug.Log("task manager not found, attempt to find...");
            Start();
        }
        taskManager.AddMetric(key, value);
    }

    public void SetTime (float time)
    {
        this.time.Add(time);
    }

    public float GetLastTimeInterval()
    {
        return time[time.Count - 1];
    }

    public List<float> GetAllTimeIntervals()
    {
        return time;
    }

}
