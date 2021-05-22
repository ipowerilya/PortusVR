using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    public float Time { get; private set; } = 0;
    public VoidEvent onStart;
    public FloatEvent onStop;
    public FloatEvent onUpdate;

    float startTime;
    bool counting = false;

    private void Update()
    {
        if (counting)
        {
            Time = ElapsedTime();
            onUpdate.Invoke(ElapsedTime());
        }
    }

    public void StartCount()
    {
        
        if (!counting)
        {
            startTime = UnityEngine.Time.time - Time;
            counting = true;
            onStart.Invoke();
        }
    }

    public void ResetCount()
    {
        startTime = UnityEngine.Time.time;
        Time = 0;
        onUpdate.Invoke(ElapsedTime());
    }

    public void StopCount()
    {
        if (counting)
        {
            onStop.Invoke(ElapsedTime());
            counting = false;
        }
    }

    private float ElapsedTime()
    {
        return counting ? UnityEngine.Time.time - startTime
                        : 0f;
    }
}
