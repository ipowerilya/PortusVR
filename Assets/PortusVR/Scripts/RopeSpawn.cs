using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{

    [SerializeField]
    GameObject partPrefab, ParentObject, ChildObject;


    [SerializeField]
    float partDistance = 0.21f;

    //[SerializeField]
    //bool reset, spawn, snapFirst, snapLast;

    // Update is called once per frame
    void Start()
    {
        Spawn(Vector3.Distance(ParentObject.transform.position, ChildObject.transform.position));
    }

    public void Spawn(float length)
    {
        int count = (int)(length / partDistance);
        for (int x = 0; x < count; ++x)
        {
            GameObject tmp;
            tmp = Instantiate(partPrefab, new Vector3(ChildObject.transform.position.x,
                                                      ChildObject.transform.position.y + partDistance * (x + 1),
                                                      ChildObject.transform.position.z), Quaternion.identity, ParentObject.transform);
            tmp.transform.eulerAngles = new Vector3(180, 0, 0);
            tmp.name = ParentObject.transform.childCount.ToString();
            if (x == 0)
            {
                //Destroy(tmp.GetComponent<CharacterJoint>());
                //tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                tmp.GetComponent<CharacterJoint>().connectedBody = ChildObject.transform.GetComponent<Rigidbody>();
            }
            else
            {
                tmp.GetComponent<CharacterJoint>().connectedBody = ParentObject.transform.Find((ParentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
            if (x+1 == count)
            {
                CharacterJoint last_joint = tmp.AddComponent<CharacterJoint>() as CharacterJoint;
                last_joint.connectedBody = ParentObject.transform.GetComponent<Rigidbody>();
            }
            
            //parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<CharacterJoint>().connectedBody = TopSnapObject.GetComponent<Rigidbody>();
        }
    }
}
