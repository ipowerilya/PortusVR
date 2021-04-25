using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskOverviewUI : MonoBehaviour
{
    public Lab lab;

    public VerticalLayoutGroup group;
    public GameObject buttonPrefab;
    public delegate void OnCurTaskIndexChange(int x);
    public OnCurTaskIndexChange TaskCallback;

    public void ClearUI()
    {
        for (int i = group.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(group.transform.GetChild(i).gameObject);
        }
    }

    public void UpdateUI(int selectedTask)
    {
        ClearUI();
        var tasks = lab.tasks;
        for (int i = 0; i < tasks.Count; ++i)
        {
            var task = tasks[i];
            var obj = Instantiate(buttonPrefab, group.transform);
            obj.GetComponentInChildren<Text>().text = task.taskName;
            obj.GetComponentInChildren<Toggle>().isOn = task.done;
            var button = obj.GetComponentInChildren<Button>();

            var currentTaskIndex = i;
            button.onClick.AddListener(() => {
                TaskCallback(currentTaskIndex);
            });

            if (i == selectedTask)
            {
                var colorBlock = button.colors;
                colorBlock.normalColor = Color.green;
                colorBlock.highlightedColor = Color.green;
                button.colors = colorBlock;
            }
        }
    }
}
