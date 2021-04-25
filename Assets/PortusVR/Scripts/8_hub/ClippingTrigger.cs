using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ClippingTrigger : MonoBehaviour
{
    Collider thisCollider;
    private void Start()
    {
        thisCollider = GetComponent<Collider>();
        if (!thisCollider.isTrigger) throw new UnityException("Clipping collider must be a trigger");
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
