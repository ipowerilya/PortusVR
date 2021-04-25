using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class FillMetricTableFromFile : MonoBehaviour
{
    static public bool GetTable(string labName, string taskName, MetricTable output_table)
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
            if (file.Contains(labName) && file.Contains(taskName) && file.Contains("username")) {
                output_table.ClearDataAndKeys();
                ReadData(file, output_table);
                return true;
            }
        }
        return false;
    }
    static bool ReadData(string file_path, MetricTable output_table)
    {
        string[] readText = File.ReadAllLines(file_path);
        InitTable(readText[0], output_table);
        for (int i = 1; i < readText.Length; ++i)
        {
            Debug.Log("Line " + i + ": " + readText[i]);
            ReadCsvLine(readText[i], output_table);
        }
        return true;
    }
    static void ReadCsvLine(string csvLine, MetricTable output_table)
    {
        string[] values = csvLine.Split(',');
        for(int i = 0; i < values.Length; ++i)
        {
            if (values[i] != "")
                output_table.rawTable[i].Add(Convert.ToSingle(values[i]));
        }
    }
    static void InitTable(string csvLine, MetricTable output_table)
    {
        Debug.Log("Header: " + csvLine);
        string[] values = csvLine.Split(',');
        foreach (string value in values)
        {
            if (value != "")
                output_table.orderedKeys.Add(value);
        }
        output_table.Awake();
    }
}
