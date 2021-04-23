using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTasksManager : MonoBehaviour
{

    public List<LabTask> tasks; // fill in order of execution
    LabTask current_task;
    int current_task_index;

    private void Start()
    {
        if (tasks.Count > 0)
        {
            current_task = tasks[0];
            current_task_index = 0;
        }
    }

    bool SwitchToNextLab()
    {
        if (current_task_index + 1 < tasks.Count)
        {
            ++current_task_index;
            current_task = tasks[current_task_index];
            return true;
        }
        return false;
    }

    void SaveResultsToFiles()
    {
        foreach (var task in tasks)
            task.SaveResultsToFile();
    }
}
