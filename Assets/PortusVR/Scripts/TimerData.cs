using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerData : MetricReading
{
    //public float Timer;
    public List<float> TimeList = new List<float>();
    public List<int> Angle = new List<int>();
    public GameObject platform;
    public GameObject TablePlaceholder;
    public GameObject TableData;
    public GameObject TableDataPrefab;
    public Text ExNumber, ExAngle, ExData;
    public float Distance, accel, speed;
    public GetDistance DistanceSrc;
    public bool Lab7;
    public Text DistText;
    public MetricReader7 Metric7;
    public void SaveData(float TimeValue)
    {
        TimeList.Add(TimeValue);
        Angle.Add((int)platform.transform.localRotation.eulerAngles.x);
        if (!Lab7)
        {
            Distance = DistanceSrc.dist;
            accel = 2 * Distance / (TimeValue * TimeValue);
            speed = accel * TimeValue;
        }
        else
        { 
            Distance = float.Parse(DistText.text);
            speed = Distance / TimeValue;
        }

        //TableData = Instantiate(TableDataPrefab, TablePlaceholder.transform);
        //foreach(Transform child in TableData.transform)
        //{
        //    if(child.gameObject.tag == "Try")
        //    {
        //        child.gameObject.GetComponent<Text>().text = TimeList.Count.ToString();
        //    }
        //    if (child.gameObject.tag == "AngleData")
        //    {
        //        child.gameObject.GetComponent<Text>().text = Angle[Angle.Count-1].ToString();
        //    }
        //    if (child.gameObject.tag == "TimeData")
        //    {
        //        child.gameObject.GetComponent<Text>().text = TimeList[TimeList.Count - 1].ToString();
        //    }
        //}
        if (!Lab7)
            ReadMetric();
        else
        {
            Metric7.ReadMetric();
        }
    }
    public override void ReadMetric()
    {
        AddMetric("Время (с)", TimeList[TimeList.Count-1]);
        AddMetric("Ускорение (м/с^2)", accel, true);
        AddMetric("Мгновенная скорость (м/с)", speed, true);
        AddMetric("Угол", Angle[Angle.Count-1]);
        AddMetric("Расстояние (м)", Distance);
    }
}
