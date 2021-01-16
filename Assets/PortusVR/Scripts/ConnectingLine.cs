using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingLine : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;

    LineRenderer line;
    void Start()
    {
        line = this.gameObject.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
    }

    void Update()
    {
        line.SetPosition(0, objectA.transform.position);
        line.SetPosition(1, objectB.transform.position);
    }
}
