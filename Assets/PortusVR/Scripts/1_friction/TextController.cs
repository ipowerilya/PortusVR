using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextController : MonoBehaviour
{
    Text text;

    public void Start()
    {
        text = GetComponent<Text>();
    }

    public void SetValue(float val)
    {
        text.text = val.ToString("0");
    }
}
