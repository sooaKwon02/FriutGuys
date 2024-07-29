using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanCtrl : MonoBehaviour
{
    float flyForce;
    public Vector3 vec = Vector3.zero;

    private void Update()
    {
        flyForce = Random.Range(0.5f, 1.0f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 flyVel = vec * flyForce;
            Debug.Log(flyVel);
            rb.AddForce(flyVel, ForceMode.Impulse);
        }
    }
}
