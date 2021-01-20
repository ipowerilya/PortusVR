using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Task : MonoBehaviour
{
    public string Name = "New Task";
    public string Description = "New Task Description";
    public float TotalScore = 0;
    public float CompletionScore = 0f;
    
    public bool IsCompleted { get; set; } = false;

    public ScoreCalculator[] ScoreCalculators;
    public float[] Weights;

    public void CalculateScore()
    {
        IsCompleted = true;
        var weightSum = Weights.Sum();

        Debug.Assert(weightSum >= 0.99 && weightSum <= 1.01);
        Debug.Assert(ScoreCalculators.Length == Weights.Length);

        TotalScore = 0;
        for (int i = 0; i < ScoreCalculators.Length && IsCompleted; ++i)
        {
            var score = ScoreCalculators[i].CalculateScore();
            IsCompleted = score >= CompletionScore;
            TotalScore += score * Weights[i];
        }
    }
}
