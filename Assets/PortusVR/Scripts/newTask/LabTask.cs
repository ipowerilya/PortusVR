using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTask : MonoBehaviour
{

    public MetricTable table;
    public string task_description;
    public string task_name;
    public string internal_task_name;
    public string internal_lab_name;

    public void SaveResultsToFile()
    {
        table.DumpLab(internal_lab_name, internal_task_name);
    }

}
