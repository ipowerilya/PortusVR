using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject SpherePrefab;
    public GameObject CurrentSphere;
    public TimerData TimerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CreatSphere()
    {
        CurrentSphere = Instantiate(SpherePrefab, SpawnPoint.position, SpawnPoint.rotation);
        CurrentSphere.GetComponent<TimerScript>().TimerObj = TimerObj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
