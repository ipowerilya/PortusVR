using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleChanger : MonoBehaviour
{
    public GameObject platformObject;
    public Text AngleData;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetAngle()
    {
        platformObject.transform.localRotation = Quaternion.Euler(int.Parse(AngleData.text), 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
