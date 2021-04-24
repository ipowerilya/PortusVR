using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public LabTasksManager taskManager;

    public Toggle donenessToggle;
    public Text taskName;
    public Text description;

    public void UpdateUI()
    {
        var task = taskManager.GetCurrentTask();
        taskName.text = task.taskName;
        description.text = task.description;
        donenessToggle.isOn = task.done;
    }
}
