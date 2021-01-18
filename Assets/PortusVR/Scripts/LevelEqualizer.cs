using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEqualizer : MonoBehaviour
{
    public MomentSum momentA;
    public MomentSum momentB;
    public float eps = 1f;
    public Rigidbody body;
    public Vector3 rotationTarget = Vector3.zero;
    public float speed = 100000f;

    void Update()
    {
        if (Mathf.Abs(momentA.moment - momentB.moment) <= eps)
        {
            Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(rotationTarget), speed);
        }
    }
}
