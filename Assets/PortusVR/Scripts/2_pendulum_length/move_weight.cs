using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_weight : MonoBehaviour
{
    public Transform point_a;
    public Transform point_b;
    public GameObject moving_point;
    private FixedJoint fixedJoint;

    // Start is called before the first frame update
    void Start()
    {
        fixedJoint = moving_point.GetComponent<FixedJoint>();
    }
    public void UpdatePosition(float percentage)
    {
        Vector3 new_position = Vector3.Lerp(point_a.position, point_b.position, percentage / 100f);
        moving_point.transform.position = new_position;
        fixedJoint.connectedAnchor = new_position;
    }
}
