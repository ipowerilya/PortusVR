using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab6SpawnObject : MonoBehaviour
{

    public Transform SpawnPoint;
    public GameObject ObjectToSpawn;
    GameObject SpawnedObject;

    public void Spawn()
    {
        SpawnedObject = Instantiate(ObjectToSpawn, SpawnPoint.position, SpawnPoint.rotation);
    }

}
