using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public TimerData TimerObj;
    public float timer;
    public bool TimerStarted = false;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("TimerStart"))
        {
            TimerChange("Start");
        }
        if(other.gameObject.CompareTag("TimerStop"))
        {
            TimerChange("Stop");
        }
        if(other.gameObject.CompareTag("DeskEnd"))
        {
            //TimerChange("Stop");
            Destroy(this.gameObject);
        }

    }
    public void TimerChange(string task)
    {
        if(task == "Start")
        {
            timer = 0;
            TimerStarted = true;
        }
        if(task == "Stop")
        {
            TimerStarted = false;
            TimerObj.SaveData(this.timer);
        }
    }
    public void Update()
    {
        if(TimerStarted == true)
        {
            timer += Time.deltaTime;
        }
    }

}
