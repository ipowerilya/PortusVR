using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sticky : MonoBehaviour
{
    public Collider target;
    Joint joint;
    float detachTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (target == collision.collider && (Time.time - detachTime) > 0.2f)
        {
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = target.attachedRigidbody;
        }
    }

    public void Detach()
    {
        detachTime = Time.time;
        Destroy(joint);
    }
}
