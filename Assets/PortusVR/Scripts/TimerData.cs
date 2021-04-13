using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerData : MonoBehaviour
{
    //public float Timer;
    public List<float> TimeList = new List<float>();
    public void SaveData(float TimeValue)
    {
        TimeList.Add(TimeValue);
    }
}
