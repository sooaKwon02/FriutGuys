using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution : MonoBehaviour
{
    public Transform target;

    public float rotSpeed = 10f;

    private void Update()
    {
        transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), rotSpeed * Time.deltaTime);
        
        if(transform.rotation.z < 0)
        {
            GetComponent<FanCtrl>().vec = new Vector3(0, 0, -1);
        }
        else if(transform.rotation.z > 0)
        {
            GetComponent<FanCtrl>().vec = new Vector3(0, 0, 1);
        }
    }
}
