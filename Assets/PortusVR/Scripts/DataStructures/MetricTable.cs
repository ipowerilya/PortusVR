using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using UnityEngine;


public class MetricTable : MonoBehaviour
{
    public List<string> orderedKeys = new List<string>();
    List<List<float>> rawTable = new List<List<float>>();
    Dictionary<string, int> keyIndex = new Dictionary<string, int>();

    bool initialized = false;
    public bool containsUnsavedData = false;

    public void Awake()
    {
        if (!initialized && orderedKeys.Count > 0)
        {
            // should be assert orderedKeys.Count > 0
            for (int i = 0; i < orderedKeys.Count; ++i)
            {
                rawTable.Add(new List<float>());
                keyIndex.Add(orderedKeys[i], i);
            }
            initialized = true;
        }
    }

    public void AddMetric(string key, float value = float.NaN)
    {

        var column = GetColumn(key);
        var index = column.IndexOf(float.NaN);
        if (index == -1)
        {
            column.Add(value);
            FillEmptyColumns();
            containsUnsavedData = true;
        }
        else
        {
            AddMetricByIndex(key, index, value);
        }
    }

    public void AddMetricByIndex(string key, int index, float value = float.NaN)
    {
        var column = GetColumn(key);
        Debug.Assert(column.Count > index);
        column[index] = value;
        containsUnsavedData = true;
    }

    void FillEmptyColumns()
    {
        int maxLength = GetMaxListCount();
        foreach (var list in rawTable)
        {
            list.AddRange(Enumerable.Repeat<float>(float.NaN, maxLength - list.Count));
        }
    }

    public int GetMaxListCount()
    {
        return rawTable.Max(table => table.Count);
    }

    List<float> GetColumn(string key)
    {
        if (keyIndex.ContainsKey(key))
        {
            return rawTable[keyIndex[key]];
        }
        else
        {
            throw new Exception("Metric key \"" + key + "\" does not exist");
        }
    }

    public IReadOnlyList<float> GetConstColumn(string key)
    {
        return GetColumn(key);
    }

    public void ClearData()
    {
        for (int i = 0; i < rawTable.Count; ++i)
        {
            rawTable[i] = new List<float>();
        }
        containsUnsavedData = false;
    }

    public void ClearDataAndKeys()
    {
        orderedKeys.Clear();
        rawTable.Clear();
        keyIndex.Clear();
        initialized = false;
    }

    public void DumpToFile(string path, char delimeter = ',', char lineSeparator = '\n')
    {
        var rowCount = GetMaxListCount();
        if (rowCount == 0) return;
        var absPath = Application.persistentDataPath + "/" + path;
        Debug.Log("dumping table " + absPath);
        var file = File.Open(absPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        file.SetLength(0); // flush
        var writer = new StreamWriter(file);
        foreach (var key in orderedKeys)
        {
            writer.Write(key);
            writer.Write(delimeter);
        }
        writer.Write(lineSeparator);

        for (int i = 0; i < rowCount; ++i)
        {
            for (int j = 0; j < rawTable.Count; ++j)
            {

                if (rawTable[j].Count > i)
                    writer.Write(rawTable[j][i].ToString(CultureInfo.GetCultureInfo("en-GB"))); //to avoid comas
                else
                    writer.Write("");
                writer.Write(delimeter);
            }
            writer.Write(lineSeparator);
        }
        writer.Close();
        containsUnsavedData = false;
    }
    public bool ReadTableFromFile(string labName, string taskName)
    {
        string[] fileEntries = Directory.GetFiles(Application.persistentDataPath + "/");
        foreach (string file_path in fileEntries)
        {
            if (file_path.Contains(labName) && file_path.Contains(taskName) && file_path.Contains("username"))
            {
                ClearDataAndKeys();
                string[] readText = File.ReadAllLines(file_path);
                ReadTableHeader(readText[0]);
                Awake();
                for (int i = 1; i < readText.Length; ++i)
                {
                    ReadMetricsLine(readText[i]);
                }
                return true;
            }
        }
        return false;
    }
    void ReadMetricsLine(string csvLine)
    {
        string[] values = csvLine.Split(',');
        for (int i = 0; i < values.Length; ++i)
        {
            if (values[i] != "")
                rawTable[i].Add(Convert.ToSingle(values[i], CultureInfo.GetCultureInfo("en-GB")));
        }
    }
    void ReadTableHeader(string csvLine)
    {
        Debug.Log("Header: " + csvLine);
        string[] values = csvLine.Split(',');
        foreach (string value in values)
        {
            if (value != "")
                orderedKeys.Add(value);
        }
    }

    public void PopRow()
    {
        foreach (var key in orderedKeys)
        {
            var column = GetColumn(key);
            column.RemoveAt(column.Count - 1);
        }
    }
}
