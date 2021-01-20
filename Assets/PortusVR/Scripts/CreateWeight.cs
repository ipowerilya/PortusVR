using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWeight : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public GameObject knob;
    public GameObject spawnPoint;
    // This script will simply instantiate the Prefab when the game starts.
    public void SpawnWeight()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        var newObject = Instantiate(myPrefab, spawnPoint.transform.position, Quaternion.identity);
        var mass_str = knob.GetComponentInChildren<Text>().text;
        newObject.GetComponent<Rigidbody>().mass = int.Parse(mass_str);
        foreach (Text text in newObject.GetComponentsInChildren<Text>())
        {
            text.text = mass_str;
        }
    }
}
