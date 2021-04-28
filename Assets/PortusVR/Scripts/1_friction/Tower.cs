using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Tower : MonoBehaviour
{
    public Transform baseObject;
    public float sectionHeight = 1;
    public int maxHeight = 3;

    public int height { get { return tower.Count; } }

    List<GameObject> tower = new List<GameObject>();

    Vector3 sectionPosition(int height)
    {
        return baseObject.transform.position + Vector3.up * sectionHeight * (height+1);
    }

    Quaternion sectionRotation()
    {
        return baseObject.transform.rotation;
    }

    void ResetSection(int height)
    {
        var obj = tower[height];
        obj.transform.position = sectionPosition(height);
        obj.transform.rotation = sectionRotation();
        var rb = obj.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePositionZ
                       | RigidbodyConstraints.FreezeRotation;
    }

    public void ResetState()
    {
        for (int height = 0; height < tower.Count; height++)
        {
            ResetSection(height);
        }
    }

    public void Push()
    {
        var height = tower.Count;
        if (height < maxHeight)
        {
            var obj = Instantiate(baseObject.gameObject);
            tower.Add(obj);
            ResetSection(height);
        }
    }

    public void Pop()
    {
        if (tower.Count != 0)
        {
            var idx = tower.Count - 1;
            var obj = tower[idx];
            tower.RemoveAt(idx);
            Destroy(obj);
        }
    }
}
