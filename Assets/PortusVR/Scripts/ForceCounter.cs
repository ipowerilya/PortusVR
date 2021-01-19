using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BNG;
using UnityEngine.UI;

public class ForceCounter : MonoBehaviour
{

    public JointSnapZone[] snap_zones;

    public Text text_output;



    // Start is called before the first frame update
    void Start()
    {
        text_output.text = "start";


    }

    // Update is called once per frame
    void Update()
    {

        text_output.text = "magnitude = " 
          + snap_zones.Select(x => x.SnappedObjectJoint)
            .Where(x => x != null)
            .Select(x => x.currentForce.magnitude).Sum();

    }
}
