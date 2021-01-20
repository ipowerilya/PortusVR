using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeverBalance : ScoreCalculator
{
    public MomentSum momentA;
    public MomentSum momentB;
    public override float CalculateScore()
    {
        var diff = Mathf.Abs(momentA.moment - momentB.moment);
        var sum = momentA.moment + momentB.moment;
        return sum <= 0.1
               ? 2
               : diff < 0.5f
               ? 5
               : diff < 1f
               ? 4
               : diff < 2f
               ? 3
               : 2;
    }
}
