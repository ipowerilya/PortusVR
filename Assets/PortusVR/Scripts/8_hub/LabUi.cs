using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabUi : MonoBehaviour
{
    public Text labName;
    public Text description;

    public void SetLab(Lab lab)
    {
        labName.text = lab.labName;
        description.text = lab.shortDescription;
    }

}
