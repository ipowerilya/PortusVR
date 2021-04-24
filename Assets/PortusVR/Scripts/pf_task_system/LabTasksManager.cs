﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTasksManager : MonoBehaviour
{
    public Lab lab; 
    int currentTaskIndex = 0;

    public List<TableUI> tableUIs;
    public TaskOverviewUI overviewUI;
    public TaskUI taskUI;

    public LabTask GetCurrentTask()
    {
        return GetTasks()[currentTaskIndex];
    }

    public void Start()
    {
        overviewUI.TaskCallback = SetCurrentTaskIndex;
    }

    public void SetLab(Lab lab)
    {
        overviewUI.lab = lab;
        this.lab = lab;
        Debug.Assert(GetTasks().Count > 0);
        UpdateUI();
    }

    public void AddMetric(string key, float value)
    {
        GetCurrentTask().table.AddMetric(key, value);
        UpdateUI();
    }

    public void ToggleDone()
    {
        var task = GetCurrentTask();
        task.done = !task.done;
        UpdateUI();
    }

    public void ResetResults()
    {
        var task = GetCurrentTask();
        task.done = false;
        task.table.ClearData();
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

    void SaveResultsToFiles()
    {
        foreach (var task in GetTasks())
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
        overviewUI.UpdateUI(currentTaskIndex);
    }

    void UpdateTaskUI()
    {
        taskUI.UpdateUI();
    }

    public List<LabTask> GetTasks()
    {
        return lab.tasks;
    }
}
