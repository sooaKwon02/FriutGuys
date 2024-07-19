using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanCtrl : MonoBehaviour
{
    float flyForce;

    private void OnTriggerEnter(Collider other)
    {
        flyForce = Random.Range(0.5f, 1.0f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(flyForce);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 flyVel = Vector3.up * flyForce;
            rb.AddForce(flyVel, ForceMode.Impulse);
        }
    }
}
