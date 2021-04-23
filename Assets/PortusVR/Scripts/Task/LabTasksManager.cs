using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTasksManager : MonoBehaviour
{
    public List<LabTask> tasks; // fill in order of execution
    int currentTaskIndex = 0;

    public List<TableUI> tableUIs;
    public TaskOverviewUI overviewUI;
    public TaskUI taskUI;

    public LabTask GetCurrentTask()
    {
        return tasks[currentTaskIndex];
    }

    public void Start()
    {
        Debug.Assert(tasks.Count > 0);
        UpdateTableUIs();
    }

    void AddMetric(string key, float value)
    {
        GetCurrentTask().table.AddMetric(key, value);
        UpdateTableUIs();
    }

    // TODO: оно здесь не должно быть наверное
    public void ToggleDone()
    {
        GetCurrentTask().done = !GetCurrentTask().done;
        UpdateTableUIs();
        UpdateOverviewUI();
    }

    public void ResetResults()
    {
        GetCurrentTask().table.ClearData();
        UpdateTableUIs();
    }

    public void SwitchToNextTask()
    {
        if (currentTaskIndex + 1 < tasks.Count)
        {
            SetCurrentTaskIndex(++currentTaskIndex);
        }
    }
    public void SwitchToPrevTask()
    {
        if (currentTaskIndex - 1 >= 0)
        {
            SetCurrentTaskIndex(--currentTaskIndex);
        }
    }

    public void SetCurrentTaskIndex(int index)
    {
        currentTaskIndex = index;
        UpdateTableUIs();
        taskUI.SetTask(GetCurrentTask());
    }
    public int GetCurrentTaskIndex()
    {
        return currentTaskIndex;
    }

    void SaveResultsToFiles()
    {
        foreach (var task in tasks)
            task.SaveResultsToFile();
    }

    void UpdateTableUIs()
    {
        foreach (var tableUI in tableUIs)
        {
            tableUI.SetTable(GetCurrentTask().table);
            tableUI.UpdateTable();
        }
    }

    void UpdateOverviewUI()
    {
        overviewUI.UpdateUI();
    }

    // GARBAGE BELOW
    public void record()
    {
        AddMetric("A", Random.Range(0, 1000));
        AddMetric("B", Random.Range(0, 1000));
    }

    public void RecordByTag(string TagName)
    {
        AddMetric(TagName, Random.Range(0, 1000));
    }
}
