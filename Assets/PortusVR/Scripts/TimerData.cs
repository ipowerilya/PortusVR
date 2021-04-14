using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerData : MonoBehaviour
{
    //public float Timer;
    public List<float> TimeList = new List<float>();
    public List<int> Angle = new List<int>();
    public GameObject platform;
    public GameObject TablePlaceholder;
    public GameObject TableData;
    public GameObject TableDataPrefab;
    public Text ExNumber, ExAngle, ExData;
    public void SaveData(float TimeValue)
    {
        TimeList.Add(TimeValue);
        Angle.Add((int)platform.transform.localRotation.eulerAngles.x);
        TableData = Instantiate(TableDataPrefab, TablePlaceholder.transform);
        foreach(Transform child in TableData.transform)
        {
            if(child.gameObject.tag == "Try")
            {
                child.gameObject.GetComponent<Text>().text = TimeList.Count.ToString();
            }
            if (child.gameObject.tag == "AngleData")
            {
                child.gameObject.GetComponent<Text>().text = Angle[Angle.Count-1].ToString();
            }
            if (child.gameObject.tag == "TimeData")
            {
                child.gameObject.GetComponent<Text>().text = TimeList[TimeList.Count - 1].ToString();
            }
        }
    }
}
