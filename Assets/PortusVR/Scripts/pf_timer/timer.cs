using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MetricReading
{
    public Text timer_text;
    bool is_stoped = true;
    public string metric_key;
    public bool readMetricOnStop = true;
    public MetricReading time_reciver = null;
    private float start_time;
    float time_diff;
    float last_added_time;

    public override void ReadMetric()
    {
        AddMetric(metric_key, time_diff);
    }
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
        if (time_diff > 1f && time_diff != last_added_time)
        {
            last_added_time = time_diff;
            if (readMetricOnStop)
                ReadMetric();
            //if (time_reciver != null)
                //time_reciver.SetTime(time_diff);
        }
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
            time_diff = Time.time - start_time;
            string min = ((int)time_diff / 60).ToString();
            string sec = (time_diff % 60).ToString("f2");
            timer_text.text = min + ":" + sec;
        }
    }
}
