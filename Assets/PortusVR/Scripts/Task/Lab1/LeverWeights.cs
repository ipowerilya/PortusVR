using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

[System.Serializable]
public class LeverWeights : ScoreCalculator
{
    public JointSnapZone SphereLeft;
    public JointSnapZone SphereRight;
    public float RequiredMassForceLeft;
    public float RequiredMassForceRight;

    public override float CalculateScore()
    {
        var forceLeft = SphereLeft.SnappedObjectJoint != null
                      ? SphereLeft.SnappedObjectJoint.currentForce.magnitude
                      : 0f;
        var forceRight = SphereRight.SnappedObjectJoint != null
                       ? SphereRight.SnappedObjectJoint.currentForce.magnitude
                       : 0f;

        var abs = Mathf.Abs((forceLeft / Physics.gravity.magnitude - RequiredMassForceLeft)
                           + (forceRight / Physics.gravity.magnitude - RequiredMassForceRight));
        var target = RequiredMassForceLeft + RequiredMassForceRight;

        Debug.Log("Diff: " + abs);

        return Mathf.Abs(abs - target) < 1f
               ? 5
               : abs < 2f
               ? 4
               : abs < 3f
               ? 3
               : 2;
    }
}
