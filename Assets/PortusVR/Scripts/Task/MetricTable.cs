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

    public void Awake()
    {
        for (int i = 0; i < orderedKeys.Count; ++i)
        {
            rawTable.Add(new List<float>());
            keyIndex.Add(orderedKeys[i], i);
        }
    }

    public void AddMetric(string key, float value = 0)
    {
        if (keyIndex.ContainsKey(key))
        {
            rawTable[keyIndex[key]].Add(value);
        }
        else
        {
            throw new Exception("Metric key \"" + key + "\" does not exist");
        }
    }

    public void DumpCSV(string path, char delimeter = ',', char lineSeparator = '\n')
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
                    writer.Write(rawTable[j][i]);
                else
                    writer.Write("");
                writer.Write(delimeter);
            }
            writer.Write(lineSeparator);
        }
        writer.Close();
    }

    public void ClearData()
    {
        for (int i = 0; i < rawTable.Count; ++i)
        {
            rawTable[i] = new List<float>();
        }
    }

    public void ClearDataAndKeys()
    {
        orderedKeys.Clear();
        rawTable.Clear();
        keyIndex.Clear();
    }

    public int GetMaxListCount()
    {
        return rawTable.Max(table => table.Count);
    }
}
