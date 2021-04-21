using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LabMenu : MonoBehaviour
{
    [Serializable]
    public class Lab
    {
        public string Name;
        public string SceneName;
    }

    public List<Lab> labs;
    public Lab selectedLab;

    public StringEvent onLabChange;

    public void SelectLab(Lab lab)
    {
        if (selectedLab != lab)
        {
            selectedLab = lab;
            onLabChange.Invoke(lab.SceneName);
        }
    }
}
