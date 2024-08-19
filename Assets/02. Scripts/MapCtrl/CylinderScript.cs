using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderScript : MonoBehaviour
{
    public float bounceForce = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounce = (collision.transform.position - transform.position).normalized;
            bounce.y = 0;

            rb.AddForce(bounce * bounceForce, ForceMode.Impulse);
        }
    }
}