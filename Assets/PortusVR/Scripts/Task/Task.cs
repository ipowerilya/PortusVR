using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Task : MonoBehaviour
{
    public string Name = "New Task";
    public string Description = "New Task Description";
    public float TotalScore = 0;
    public float completionScore = 0f;
    
    public bool IsCompleted { get; set; } = false;

    public ScoreCalculator[] scoreCalculators;
    public float[] weights;

    public void CalculateScore()
    {
        IsCompleted = true;
        var weightSum = weights.Sum();

        Debug.Assert(weightSum >= 0.99 && weightSum <= 1.01);
        Debug.Assert(scoreCalculators.Length == weights.Length);

        TotalScore = 0;
        for (int i = 0; i < scoreCalculators.Length && IsCompleted; ++i)
        {
            var score = scoreCalculators[i].CalculateScore();
            IsCompleted = score >= completionScore;
            TotalScore += score * weights[i];
        }
    }
}
