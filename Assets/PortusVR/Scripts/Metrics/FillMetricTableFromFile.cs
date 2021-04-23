using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class FillMetricTableFromFile : MonoBehaviour
{

    public MetricTable table;

    public void GetLab(string LabName)
    {
        string[] fileEntries = Directory.GetFiles(Application.persistentDataPath + "/");
        foreach (string file in fileEntries)
        {
/*            // not used for now, choosing last file by date in the name
            Debug.Log("file: " + file + " length " + file.Length);
            DateTime convertedDate;
            try
            {
                var time_str = file.Substring((file.Length - 23), 19); //date takes 19 chars
                Debug.Log(time_str);
                convertedDate = Convert.ToDateTime(time_str);
                Debug.Log("date:" + convertedDate.Kind.ToString());
            }
            catch (FormatException)
            {
                Debug.Log("unable to parse file name " + file);
            }*/
            table.ClearDataAndKeys();
            if (file.Contains(LabName) && file.Contains("username")) {
                ReadData(file);
            }
        }
    }
    bool ReadData(string file_path)
    {
        string[] readText = File.ReadAllLines(file_path);
        InitTable(readText[0]);
        for (int i = 1; i < readText.Length; ++i)
        {
            Debug.Log("Line " + i + ": " + readText[i]);
            ReadCsvLine(readText[i]);
        }
        return true;
    }
    void ReadCsvLine(string csvLine)
    {
        string[] values = csvLine.Split(',');
        for(int i = 0; i < values.Length; ++i)
        {
            if (values[i] != "")
                table.rawTable[i].Add(Convert.ToSingle(values[i]));
        }
    }
    void InitTable(string csvLine)
    {
        Debug.Log("Header: " + csvLine);
        string[] values = csvLine.Split(',');
        foreach (string value in values)
        {
            if (value != "")
                table.orderedKeys.Add(value);
        }
        table.Awake();
    }
}
