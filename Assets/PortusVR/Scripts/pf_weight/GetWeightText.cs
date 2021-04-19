using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWeightText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Text text in this.GetComponentsInChildren<Text>())
        {
            text.text = this.GetComponent<Rigidbody>().mass.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
