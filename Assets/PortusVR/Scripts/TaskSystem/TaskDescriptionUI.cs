using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDescriptionUI : MonoBehaviour
{
    public LabTasksManager taskManager;

    public Toggle donenessToggle;
    public Text taskName;
    public Text description;

    public void UpdateUI()
    {
        var task = taskManager.GetCurrentTask();
        taskName.text = task.taskName;
        description.text = task.description + (task.description.EndsWith("\n") ? "" : "\n");
        donenessToggle.isOn = task.done;
    }
}
