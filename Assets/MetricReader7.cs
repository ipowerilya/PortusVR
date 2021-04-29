using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricReader7 : MetricReading
{
    public TimerData TmData;
    public override void ReadMetric()
    {
        AddMetric("Время (с)", TmData.TimeList[TmData.TimeList.Count - 1]);
        AddMetric("Расстояние (м)", TmData.Distance);
        AddMetric("Скорость (м/с)", TmData.speed);
        
    }
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
