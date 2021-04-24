using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabOverviewUI : MonoBehaviour
{
    public TaskOverviewUI tasksOverview;
    public LabUi labUi;
    public TableUI resultsUi;

    int currentTaskIndex = 0;
    Lab lab;

    private void Start()
    {
        tasksOverview.TaskCallback = SetCurrentTaskIndex;
    }

    public void SetLab(Lab lab)
    {
        currentTaskIndex = 0;
        this.lab = lab;
        tasksOverview.lab = lab;
        tasksOverview.UpdateUI();
        UpdateUI();
    }

    public void SetCurrentTaskIndex(int index)
    {
        currentTaskIndex = index;
        UpdateUI();
    }

    public void UpdateUI()
    {
        labUi.SetLab(lab);
        resultsUi.SetTable(lab.tasks[currentTaskIndex].table);
        resultsUi.UpdateTable();
    }
}
