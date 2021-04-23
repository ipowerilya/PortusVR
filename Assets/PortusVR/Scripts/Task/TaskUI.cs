using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    LabTask task;
    public Toggle donenessToggle;
    public Text name;
    public Text description;

    public void SetTask(LabTask task)
    {
        this.task = task;
        donenessToggle.isOn = task.done;
    }

    public void ToggleDone()
    {
        task.done = !task.done;
        UpdateUI();
    }

    public void UpdateUI()
    {
        donenessToggle.isOn = task.done;

    }
}
