using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public LabTasksManager taskManager;

    public Toggle donenessToggle;
    public Text name;
    public Text description;

    public void UpdateUI()
    {
        var task = taskManager.GetCurrentTask();
        name.text = task.name;
        description.text = task.description;
        donenessToggle.isOn = task.done;
    }
}
