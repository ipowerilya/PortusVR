using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetForceData : MonoBehaviour
{
    public Joint JointTest;
    public Text txt;
    public float magnitData;
    //public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        magnitData = JointTest.currentForce.magnitude;
        
        //JointTest.connectedBody = rb;
        txt.text = "Force " + magnitData.ToString("0.00");
    }
}
