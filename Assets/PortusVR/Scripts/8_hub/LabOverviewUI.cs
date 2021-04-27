using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabOverviewUI : MonoBehaviour
{
    public TaskSelectionUI tasksSelectionUI;
    public LabDescriptionUI labDescriptionUi;
    public MetricTableRepresentation metricTableRepresentation;

    int currentTaskIndex = 0;
    Lab lab;

    private void Start()
    {
        tasksSelectionUI.TaskCallback = SetCurrentTaskIndex;
    }

    public void SetLab(Lab lab)
    {
        currentTaskIndex = 0;
        this.lab = lab;
        tasksSelectionUI.lab = lab;
        UpdateUI();
    }

    public void SetCurrentTaskIndex(int index)
    {
        currentTaskIndex = index;
        UpdateUI();
    }

    public void UpdateUI()
    {
        labDescriptionUi.SetLab(lab);
        metricTableRepresentation.SetTable(lab.tasks[currentTaskIndex].table);
        metricTableRepresentation.UpdateTable();
        tasksSelectionUI.UpdateUI(currentTaskIndex);
    }
}
