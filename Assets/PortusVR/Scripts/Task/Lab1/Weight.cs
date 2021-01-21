using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weight : ScoreCalculator
{
    public Joint joint;
    public float targetForce;

    public override float CalculateScore()
    {
        float force = joint.currentForce.magnitude;
        float diff = Mathf.Abs(force - targetForce);
        return diff < 1.0f
               ? 5
               : diff < 2.0f
               ? 4
               : diff < 3.0f
               ? 3
               : 2;
    }
}
