using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrTask : MonoBehaviour
{
    public List<string> TaskList = new List<string>();
    public List<bool> ComplitionList = new List<bool>();
    public List<Toggle> CompToggles = new List<Toggle>();
    public Text TaskText;
    public int CurrentTask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TaskText.text = TaskList[CurrentTask];
        if(ComplitionList[CurrentTask])
        {
            TaskText.color = Color.green;
            CompToggles[CurrentTask].isOn = true;
        }
        else
        {
            TaskText.color = Color.red;
            CompToggles[CurrentTask].isOn = false;
        }
    }
    public void TaskIsDone(int TaskNumb)
    {
        ComplitionList[TaskNumb] = true;
    }
    public void MoveTask(int MoveValue)
    {
        if(MoveValue < 0 && CurrentTask >0)
        {
            CurrentTask--;
        }
        else if (MoveValue > 0 && CurrentTask < TaskList.Count - 1)
        {
            CurrentTask++;
        }
    }
}
