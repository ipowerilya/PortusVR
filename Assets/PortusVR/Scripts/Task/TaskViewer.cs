using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskViewer : MonoBehaviour
{
    public List<Task> TaskList = new List<Task>();
    public List<Toggle> Toggles = new List<Toggle>();
    public int CurrentTaskIndex = 0;

    public Text TaskNameText;
    public Text TaskDescriptionText;
    public Text ScoreText;

    public void CalculateScore()
    {
        var currentTask = TaskList[CurrentTaskIndex];
        currentTask.CalculateScore();
        updateUI();
        Toggles[CurrentTaskIndex].isOn = currentTask.IsCompleted;
    }

    private void updateUI()
    {
        var currentTask = TaskList[CurrentTaskIndex];
        TaskNameText.text = (CurrentTaskIndex + 1).ToString() + ": " + currentTask.Name;
        TaskDescriptionText.text = currentTask.Description;
        ScoreText.text = "Ваш скор: " + currentTask.TotalScore;
        ScoreText.color = currentTask.IsCompleted
                        ? Color.green
                        : Color.red;
    }

    public void MoveTask(int MoveValue)
    {
        CurrentTaskIndex = Mathf.Clamp(CurrentTaskIndex + MoveValue, 0, TaskList.Count - 1);
        updateUI();
    }
}
