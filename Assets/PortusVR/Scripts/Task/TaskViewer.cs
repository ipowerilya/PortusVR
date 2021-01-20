using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskViewer : MonoBehaviour
{
    public List<Task> TaskList = new List<Task>();
    public List<Toggle> Toggles = new List<Toggle>();
    public int CurrentTaskIndex = 0;

    public Text TaskText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CalculateScore()
    {
        TaskList[CurrentTaskIndex].CalculateScore();
        TaskText.text = "Your score: " + TaskList[CurrentTaskIndex].TotalScore;
    }

    // Update is called once per frame
    /*void Update()
    {
        var currentTask = TaskList[CurrentTaskIndex];
        TaskText.text = currentTask.Description;
        if(ComplitionList[CurrentTaskIndex])
        {
            TaskText.color = Color.green;
            Toggles[CurrentTaskIndex].isOn = true;
        }
        else
        {
            TaskText.color = Color.red;
            Toggles[CurrentTaskIndex].isOn = false;
        }
    }
    public void TaskIsDone(int TaskNumb)
    {
        ComplitionList[TaskNumb] = true;
    }
    public void MoveTask(int MoveValue)
    {
        if(MoveValue < 0 && CurrentTaskIndex >0)
        {
            CurrentTaskIndex--;
        }
        else if (MoveValue > 0 && CurrentTaskIndex < TaskList.Count - 1)
        {
            CurrentTaskIndex++;
        }
    }*/
}
