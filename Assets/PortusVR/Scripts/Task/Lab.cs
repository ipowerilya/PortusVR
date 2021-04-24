using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab : MonoBehaviour
{

    public string labName;
    public string internalName; //scene should be named the same
    public bool done = false; // TODO move to method
    public List<LabTask> tasks; // fill in order of execution
    public string shortDescription;
    public Material associatedSkybox;
    // TODO add description with pics format

    public void InitTasks()
    {
        foreach (var task in tasks)
        {
            task.Initialize(internalName);
        }
    }
}
