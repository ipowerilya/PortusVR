using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject SpherePrefab;
    public GameObject CurrentSphere;
    public TimerData TimerObj;
    public bool VelocitySpawner;
    public Text Txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CreatSphere()
    {
        CurrentSphere = Instantiate(SpherePrefab, SpawnPoint.position, SpawnPoint.rotation);
        CurrentSphere.GetComponent<TimerScript>().TimerObj = TimerObj;
        if(VelocitySpawner == true)
        {
            CurrentSphere.GetComponent<ConstantVelos>().ConstantVelocity = int.Parse(Txt.text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
