using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sticky : MonoBehaviour
{
    public Collider target;
    Joint joint = null;
    float detachTime = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        var timeDiff = Time.time - detachTime;
        Debug.Log("time: " + timeDiff.ToString());
        if (target == collision.collider && joint == null && timeDiff > 0.2f)
        {
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = target.attachedRigidbody;
        }
    }

    public void Detach()
    {
        detachTime = Time.time;
        Destroy(joint);
        joint = null;
    }
}
