using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pendulum_fix_release : MonoBehaviour
{
    public GameObject moving_part;

    public void Freeze()
    {
        moving_part.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Release()
    {
        moving_part.GetComponent<Rigidbody>().isKinematic = false;
    }
}
