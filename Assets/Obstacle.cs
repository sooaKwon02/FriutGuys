using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    float rotationTan=60;
    Rigidbody obj;
    private void Awake()
    {
        obj=GetComponentInChildren<Rigidbody>();
    }

    void FixedUpdate()
    {
        float smoothT = Mathf.PingPong(Time.time*speed, 1.0f);
        float zRotation = Mathf.Lerp(-rotationTan, rotationTan, Mathf.SmoothStep(0.0f, 1.0f, smoothT));
        transform.eulerAngles = new Vector3(0, 0, zRotation);
        obj.velocity=obj.transform.position;
        Debug.Log(obj.velocity);
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(rb.velocity - transform.eulerAngles);
        }
    }
}


