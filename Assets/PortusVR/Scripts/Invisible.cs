using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    void Start()
    {
        foreach (var render in GetComponentsInChildren<Renderer>())
        {
            render.material.renderQueue = 2002;
        }
    }
}
