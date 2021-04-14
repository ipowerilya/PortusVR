using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text timer_text;
    bool is_stoped = true;

    private float start_time;
    private string output;

    LineRenderer line;
    void Start()
    {
        ResetTime();
    }

    void ResetTime()
    {
        start_time = Time.time;
    }

    public void Stop()
    {
        Update();
        is_stoped = true;
    }

    public void Continue()
    {
        is_stoped = false;
        ResetTime();
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_stoped)
        {
            float time_diff = Time.time - start_time;
            string min = ((int)time_diff / 60).ToString();
            string sec = (time_diff % 60).ToString("f2");
            timer_text.text = min + ":" + sec;
        }
    }
}
