using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabDescriptionUI : MonoBehaviour
{
    public Text labName;
    public Text description;
    public GameObject autoComputationBtn;

    public void SetLab(Lab lab)
    {
        labName.text = lab.labName;
        description.text = lab.shortDescription + (lab.shortDescription.EndsWith("\n") ? "" : "\n");
        var toggle = autoComputationBtn.GetComponentInChildren<Toggle>();
        toggle.isOn = lab.enabledAutoComputation;
    }
}
