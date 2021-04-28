using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FloatText : MonoBehaviour
{
    public string format = "0.00";
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void SetFloat(float value)
    {
        text.text = value.ToString(format);
    }
}
