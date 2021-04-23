using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskOverviewUI : MonoBehaviour
{
    public LabTasksManager taskManager;

    public VerticalLayoutGroup group;
    public GameObject buttonPrefab;

    public void Start()
    {
        UpdateUI();
    }

    public void ClearUI()
    {
        for (int i = group.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(group.transform.GetChild(i).gameObject);
        }
    }

    public void UpdateUI()
    {
        ClearUI();
        var tasks = taskManager.tasks;
        for (int i = 0; i < tasks.Count; ++i)
        {
            var task = tasks[i];
            var obj = Instantiate(buttonPrefab, group.transform);
            obj.GetComponentInChildren<Text>().text = task.taskName;
            obj.GetComponentInChildren<Toggle>().isOn = task.done;
            var button = obj.GetComponentInChildren<Button>();

            var currentTaskIndex = i;
            button.onClick.AddListener(() => {
                taskManager.SetCurrentTaskIndex(currentTaskIndex);
            });
            
            // добавить цвет выделения
            // bool selected = i == taskManager.GetCurrentTaskIndex();
        }
    }
}
