using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaxPopUp : MonoBehaviour
{
    //public GameObject RelaxPrefab;
    public GameObject RelaxObject;
    public float TimeLeft;
    public bool IsRelaxing;
    public float Distance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RelaxStart()
    {
        //RelaxObject = Instantiate(RelaxPrefab, Camera.main.transform);
        RelaxObject.SetActive(true);
        RelaxObject.transform.parent = Camera.main.transform;
        RelaxObject.transform.localPosition = new Vector3(0, 0, Distance);
        RelaxObject.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        RelaxObject.transform.localRotation = Quaternion.identity;
    }
    public void StopRelaxing()
    {
        IsRelaxing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsRelaxing)
        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 0)
        {
            TimeLeft = 15;
            IsRelaxing = true;
            RelaxStart();
        }
    }
}
