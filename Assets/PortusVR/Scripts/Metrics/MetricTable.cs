using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class MetricTable : MonoBehaviour
{
    public List<string> orderedKeys = new List<string>();
    public List<List<float>> rawTable = new List<List<float>>();
    Dictionary<string, int> keyIndex = new Dictionary<string, int>();

    void Awake()
    {
        for (int i = 0; i < orderedKeys.Count; ++i)
        {
            rawTable.Add(new List<float>());
            keyIndex.Add(orderedKeys[i], i);
        }
    }

    public void AddMetric(string key, float value = 0)
    {
        rawTable[keyIndex[key]].Add(value);
    }

    public void DumpCSV(string path, char delimeter = ',', char lineSeparator = '\n')
    {
        var rowCount = rawTable[0].Count;
        if (rowCount == 0) return;

        var absPath = Application.persistentDataPath + "/" + path;
        Debug.Log("dumping table " + absPath);

        var file = File.Open(absPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
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
                writer.Write(rawTable[j][i]);
                writer.Write(delimeter);
            }
            writer.Write(lineSeparator);
        }

        writer.Close();
    }

    private void Clear()
    {
        for (int i = 0; i < rawTable.Count; ++i)
        {
            rawTable[i] = new List<float>();
        }
    }

    public void DumpLab(string labName)
    {
        var file_name = "username" + "-" + labName + "-" + DateTime.Now.ToString("yyyy-MM-dd-H-mm-ss") + ".csv";
        DumpCSV(file_name);
        Clear();
    }

    public int GetMaxListCount()
    {
        return rawTable.Max(Table => Table.Count);
    }
}
