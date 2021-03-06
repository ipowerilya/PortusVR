using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTasksManager : MonoBehaviour
{
    public Lab lab; 
    int currentTaskIndex = 0;

    public List<MetricTableRepresentation> metricTableRepresentations;
    public TaskSelectionUI taskSelection;
    public TaskDescriptionUI taskDescription;
    public TaskDescriptionUI pocketTaskDescription;

    public LabTask GetCurrentTask()
    {
        return GetTasks()[currentTaskIndex];
    }

    public void Start()
    {
        taskSelection.TaskCallback = SetCurrentTaskIndex;
    }

    public void SetLab(Lab lab)
    {
        taskSelection.lab = lab;
        this.lab = lab;
        Debug.Assert(GetTasks().Count > 0);
        UpdateUI();
    }

    public void AddMetric(string key, float value)
    {
        GetCurrentTask().table.AddMetric(key, value);
        UpdateUI();
    }

    public void AddMetricByIndex(string key, int index, float value)
    {
        GetCurrentTask().table.AddMetricByIndex(key, index, value);
        UpdateUI();
    }

    public void ToggleDone()
    {
        var task = GetCurrentTask();
        task.done = !task.done;
        UpdateUI();
    }

    public void DeleteCurrentLabData()
    {
        GetCurrentTask().DeleteResults();
        UpdateUI();
    }

    public void SwitchToNextTask()
    {
        if (currentTaskIndex + 1 < GetTasks().Count)
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
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateTableUIs();
        UpdateTaskUI();
        UpdateOverviewUI();
    }

    public int GetCurrentTaskIndex()
    {
        return currentTaskIndex;
    }

    public void SaveAllResultsToFiles()
    {
        foreach (var task in GetTasks())
            task.SaveResultsToFile();
    }

    void UpdateTableUIs()
    {
        foreach (var tableUI in metricTableRepresentations)
        {
            tableUI.SetTable(GetCurrentTask().table);
            tableUI.UpdateTable();
        }
    }

    void UpdateOverviewUI()
    {
        taskSelection.UpdateUI(currentTaskIndex);
    }

    void UpdateTaskUI()
    {
        taskDescription.UpdateUI();
        pocketTaskDescription.UpdateUI();
    }

    public List<LabTask> GetTasks()
    {
        return lab.tasks;
    }
}
